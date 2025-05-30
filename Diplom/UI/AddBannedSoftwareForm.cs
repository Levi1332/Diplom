using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Diplom.Repository;

namespace Diplom.UI
{
    public partial class AddBannedSoftwareForm : Form
    {
        private readonly BannedSoftwareRepository repository;
        private static readonly string connectionString = DatabaseConfig.connectionString;

        public AddBannedSoftwareForm()
        {
            InitializeComponent();
            this.Icon = new Icon(Application.StartupPath + @"\Resources\icon.ico");

            repository = new BannedSoftwareRepository(connectionString);
            LoadProcesses();
        }

        private void LoadProcesses()
        {
            lstBannedProcesses.Items.Clear();
            var processes = repository.LoadBannedProcesses();
            lstBannedProcesses.Items.AddRange(processes.ToArray());
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string processName = txtProcessName.Text.Trim().ToLower();

            if (string.IsNullOrWhiteSpace(processName))
            {
                MessageBox.Show("Введите имя исполняемого файла.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!repository.AddBannedProcess(processName))
            {
                MessageBox.Show("Этот процесс уже в списке.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            lstBannedProcesses.Items.Add(processName);
            txtProcessName.Clear();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lstBannedProcesses.SelectedItem is string selected)
            {
                repository.RemoveBannedProcess(selected);
                lstBannedProcesses.Items.Remove(selected);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
