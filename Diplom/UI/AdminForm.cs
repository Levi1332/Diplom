using System;
using System.Data;
using System.Windows.Forms;
using Diplom.Services;
using Diplom.Core.Service;

namespace Diplom.UI
{
    public partial class AdminForm : Form
    {
        private readonly IUserService _userService;

        public AdminForm()
        {
            InitializeComponent();
            _userService = new UserService(); 
            LoadUsers();
            RegisterEvents();
        }

        private void LoadUsers()
        {
            foreach (DataGridViewColumn column in dataGridViewUsers.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            }

            var users = _userService.GetAllUsers();
            dataGridViewUsers.DataSource = users;
            dataGridViewUsers.Columns["TodayWorkMinutes"].HeaderText = "Время сегодня (мин)";
            dataGridViewUsers.Columns["TotalWorkMinutes"].HeaderText = "Общее время (мин)";

            if (dataGridViewUsers.Columns.Contains("PasswordHash"))
                dataGridViewUsers.Columns["PasswordHash"].Visible = false;

            if (dataGridViewUsers.Columns.Contains("Salt"))
                dataGridViewUsers.Columns["Salt"].Visible = false;

            foreach (DataGridViewColumn column in dataGridViewUsers.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
        }


        private void RegisterEvents()
        {
            btnAddUser.Click += BtnAddUser_Click;
            btnEditUser.Click += BtnEditUser_Click;
        }

        private void BtnAddUser_Click(object sender, EventArgs e)
        {
            using (var form = new UserEditForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                   
                    LoadUsers();
                }
            }
        }

        private void BtnEditUser_Click(object sender, EventArgs e)
        {
            if (dataGridViewUsers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите пользователя для редактирования.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedRow = dataGridViewUsers.SelectedRows[0];
            string userLogin = selectedRow.Cells["Login"].Value.ToString();
            string fullName = selectedRow.Cells["FullName"].Value.ToString();
            string Email = selectedRow.Cells["Email"].Value.ToString();
            string CreatedAt = selectedRow.Cells["CreatedAt"].Value.ToString();
            var user = _userService.GetUserByLogin(userLogin);

            using (var form = new UserEditForm(user,fullName,Email,CreatedAt))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    
                    LoadUsers();
                }
            }
        }
        private void BtnBanUser_Click(object sender, EventArgs e)
        {
            if (dataGridViewUsers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите пользователя для блокировки.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int userId = Convert.ToInt32(dataGridViewUsers.SelectedRows[0].Cells["UserID"].Value);

            var reasons = _userService.GetBanReasons();
            using (var form = new BanReasonForm(reasons))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    _userService.BanUser(userId, form.SelectedBanReasonId, form.AdminNote);
                    LoadUsers();
                }
            }
        }
        private void BtnDeleteUser_Click(object sender, EventArgs e)
        {
            if (dataGridViewUsers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите пользователя для удаления.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show("Удаление ползователя приведет к удалению всех данных о пользователе. Вы уверены, что хотите удалить пользователя?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            string login = dataGridViewUsers.SelectedRows[0].Cells["Login"].Value.ToString();

            try
            {
                _userService.DeleteUserByLogin(login);
                LoadUsers();
                MessageBox.Show("Пользователь успешно удалён.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnUnbanUser_Click(object sender, EventArgs e)
        {
            if (dataGridViewUsers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите пользователя для разбанивания.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var userId = Convert.ToInt32(dataGridViewUsers.SelectedRows[0].Cells["UserID"].Value);
            _userService.SetUserBanStatus(userId, false);
            LoadUsers();
        }
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = textBoxSearch.Text.Trim();

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                LoadUsers(); 
                return;
            }

            var filtered = _userService.SearchUsers(searchTerm);
            dataGridViewUsers.DataSource = filtered;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadUsers();
        }
    }
}
