using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Diplom.UI
{
    public partial class ExportPeriodForm : Form
    {
        public ExportRange SelectedRange { get; private set; }
        public DateTime CustomFrom { get; private set; }
        public DateTime CustomTo { get; private set; }
        public string ExportPath { get; private set; }


        private string fileNameTemplate;

        public ExportPeriodForm(string defaultPath)
        {
            InitializeComponent();
     
            if (!string.IsNullOrEmpty(defaultPath))
            {
                ExportPath = Path.Combine(defaultPath, $"Отчет_{DateTime.Today:yyyyMMdd}.pdf");
                lblPath.Text = ExportPath;
            }
            this.Icon = new Icon(Application.StartupPath + @"\Resources\icon.ico");
        }

        private void ExportPeriodForm_Load(object sender, EventArgs e)
        {
            radioWeek.Checked = true;

            var settings = SettingsManager.LoadSettings();
            fileNameTemplate = settings.PdfFileNameTemplate;

            if (string.IsNullOrWhiteSpace(fileNameTemplate))
                fileNameTemplate = "Отчет_{Дата}.pdf"; 
        }

        private void radioCustom_CheckedChanged(object sender, EventArgs e)
        {
            bool enabled = radioCustom.Checked;
            dateFrom.Enabled = enabled;
            dateTo.Enabled = enabled;
        }

        private void btnSelectPath_Click(object sender, EventArgs e)
        {
            var settings = SettingsManager.LoadSettings();
            string fileName = GenerateFileNameFromTemplate();

            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Filter = "PDF файлы (*.pdf)|*.pdf";
                dialog.FileName = fileName;

                if (!string.IsNullOrWhiteSpace(settings.DefaultExportPath) && Directory.Exists(settings.DefaultExportPath))
                {
                    dialog.InitialDirectory = settings.DefaultExportPath;
                }

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    ExportPath = dialog.FileName;
                    lblPath.Text = ExportPath;
                }
            }
        }

        private string GenerateFileNameFromTemplate()
        {
            string result = fileNameTemplate;

            result = result.Replace("{Дата}", DateTime.Now.ToString("yyyy-MM-dd"));
            result = result.Replace("{ДатаКоротко}", DateTime.Now.ToString("yyyyMMdd"));
            result = result.Replace("{Время}", DateTime.Now.ToString("HHmmss"));

            return result.EndsWith(".pdf") ? result : result + ".pdf";
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ExportPath))
            {
                MessageBox.Show("Пожалуйста, выберите путь для сохранения файла.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (radioWeek.Checked)
                SelectedRange = ExportRange.Week;
            else if (radioMonth.Checked)
                SelectedRange = ExportRange.Month;
            else if (radioYear.Checked)
                SelectedRange = ExportRange.Year;
            else
            {
                SelectedRange = ExportRange.Custom;
                CustomFrom = dateFrom.Value.Date;
                CustomTo = dateTo.Value.Date;
                if (CustomFrom > CustomTo)
                {
                    MessageBox.Show("Дата начала не может быть позже даты окончания.", "Ошибка диапазона", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
