using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Diplom.Core.Service;

namespace Diplom.UI
{
    public partial class EmployeeStatisticsForm : Form
    {
        private readonly IUserService _userService;
        private WorkTimeSettings _settings;
        public EmployeeStatisticsForm()
        {
            InitializeComponent();
            _userService = new UserService();
            this.Icon = new Icon(Application.StartupPath + @"\Resources\icon.ico");
            LoadUsers();
            this.AcceptButton = buttonSearch;
            dataGridViewStats.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewStats.MultiSelect = true;
            _settings = SettingsManager.LoadSettings();

        }

        private void LoadUsers()
        {
            foreach (DataGridViewColumn column in dataGridViewStats.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            }

           
            var fromDate = dateTimeFrom.Value.Date;
            var toDate = dateTimeTo.Value.Date.AddDays(1).AddTicks(-1);

          
            var users = _userService.GetAllUsers(fromDate, toDate);
            dataGridViewStats.DataSource = users;

            
            foreach (DataGridViewColumn column in dataGridViewStats.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
        }

        private void TextBox_Enter(object sender, EventArgs e)
        {
            if (textBoxSearch.Text != null)
            {
                if (textBoxSearch.Text == "Поиск по имени...")
                {
                    textBoxSearch.Text = "";
                    
                }
            }
        }
        private void TextBox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxSearch.Text))
            {
                textBoxSearch.Text = "Поиск по имени...";
                textBoxSearch.ForeColor = Color.Gray;
            }
            
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            LoadUsers();
        }
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = textBoxSearch.Text.Trim();

            if (searchTerm == "Поиск по имени...")
            {
                LoadUsers();
                return;
            }

            var fromDate = dateTimeFrom.Value.Date;
            var toDate = dateTimeTo.Value.Date.AddDays(1).AddTicks(-1);
            var filtered = _userService.SearchUsers(searchTerm, fromDate, toDate);
            dataGridViewStats.DataSource = filtered;
        }
        private void BtnExportPdf_Click(object sender, EventArgs e)
        {
            if (dataGridViewStats.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите хотя бы одного пользователя для экспорта отчёта.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedRows = dataGridViewStats.SelectedRows.Cast<DataGridViewRow>().ToList();
            DateTime from = dateTimeFrom.Value.Date;
            DateTime to = dateTimeTo.Value.Date.AddDays(1).AddTicks(-1);

            using (var saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "PDF файлы (*.pdf)|*.pdf";
                saveDialog.FileName = selectedRows.Count == 1
                    ? $"Отчет_{selectedRows[0].Cells["ФИО"].Value}_{DateTime.Now:yyyyMMdd}.pdf"
                    : $"ОбщийОтчет_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    var pdfService = new PdfReportService(
                        new WorkSessionService(DatabaseConfig.connectionString),
                        new ViolationService(DatabaseConfig.connectionString),
                        new OvertimeService(DatabaseConfig.connectionString)
                    );

                    var progressForm = new ProgressForm();
                    var progress = new Progress<int>(percent =>
                    {
                        progressForm.Invoke((Action)(() => progressForm.SetProgress(percent)));
                    });

                    var filePath = saveDialog.FileName;

                    Task.Run(() =>
                    {
                        try
                        {
                            if (selectedRows.Count == 1)
                            {
                                int userId = Convert.ToInt32(selectedRows[0].Cells["ID"].Value);
                                string fullName = selectedRows[0].Cells["ФИО"].Value.ToString();

                                var singleUserViolationService = new ViolationService(DatabaseConfig.connectionString, userId);
                                var singleUserPdfService = new PdfReportService(
                                    new WorkSessionService(DatabaseConfig.connectionString),
                                    singleUserViolationService,
                                    new OvertimeService(DatabaseConfig.connectionString)
                                );

                                singleUserPdfService.ExportFullReport(userId, from, to, filePath, progress, fullName);
                            }
                            else
                            {
                                var userIds = selectedRows.Select(r => Convert.ToInt32(r.Cells["ID"].Value)).ToList();
                                var userNames = selectedRows.Select(r => r.Cells["ФИО"].Value.ToString()).ToList();
                                pdfService.ExportCombinedReport(userIds, from, to, filePath, progress, userNames);
                            }

                            Invoke((Action)(() =>
                            {
                                progressForm.Close();
                                MessageBox.Show("Отчёт успешно сохранён.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information); 
                                if (_settings.OpenPdfAfterExport)
                                {
                                    IPdfReportService pdfReportService = new PdfReportService();
                                    pdfReportService.OpenPdfFile(filePath);
                                }
                               
                            }));
                        }
                        catch (Exception ex)
                        {
                            Invoke((Action)(() =>
                            {
                                progressForm.Close();
                                MessageBox.Show($"Ошибка при экспорте: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }));
                        }
                    });

                    progressForm.ShowDialog();
                }
            }
        }

        private void BtnExportAllPdf_Click(object sender, EventArgs e)
        {
            if (dataGridViewStats.Rows.Count == 0)
            {
                MessageBox.Show("Нет данных для экспорта.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "PDF файлы (*.pdf)|*.pdf";
                saveDialog.FileName = $"Отчет_все_сотрудники_{DateTime.Now:yyyyMMdd}.pdf";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    DateTime from = dateTimeFrom.Value.Date;
                    DateTime to = dateTimeTo.Value.Date.AddDays(1).AddTicks(-1);

                    var userIds = new List<int>();
                    var userNames = new List<string>();

                    foreach (DataGridViewRow row in dataGridViewStats.Rows)
                    {
                        if (row.IsNewRow) continue;
                        if (int.TryParse(row.Cells["ID"].Value?.ToString(), out int id))
                            userIds.Add(id);

                        userNames.Add(row.Cells["ФИО"].Value?.ToString() ?? $"ID {id}");
                    }

                    var pdfService = new PdfReportService(
                        new WorkSessionService(DatabaseConfig.connectionString),
                        new ViolationService(DatabaseConfig.connectionString),
                        new OvertimeService(DatabaseConfig.connectionString)
                    );

                    var progressForm = new ProgressForm();
                    var progress = new Progress<int>(percent =>
                    {
                        progressForm.Invoke((Action)(() => progressForm.SetProgress(percent)));
                    });
                    var filePath = saveDialog.FileName;
                    Task.Run(() =>
                    {
                        try
                        {
                            pdfService.ExportCombinedReport(userIds, from, to, saveDialog.FileName, progress, userNames);
                            Invoke((Action)(() =>
                            {
                                progressForm.Close();
                                MessageBox.Show("Отчёт по всем сотрудникам успешно сохранён.", "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information); 
                                if (_settings.OpenPdfAfterExport)
                                {
                                    IPdfReportService pdfReportService = new PdfReportService();
                                    pdfReportService.OpenPdfFile(filePath);
                                }
                            }));
                        }
                        catch (Exception ex)
                        {
                            Invoke((Action)(() =>
                            {
                                progressForm.Close();
                                MessageBox.Show($"Ошибка при генерации отчёта: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }));
                        }
                    });

                    progressForm.ShowDialog();
                }
            }
        }
    }
}


