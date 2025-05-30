using System;
using System.Data;
using System.Windows.Forms;
using Diplom.Services;
using Diplom.Core.Service;
using System.Drawing;
using Diplom.Helpers;
namespace Diplom.UI
{
    public partial class AdminForm : Form
    {
        private readonly IUserService _userService;
        private readonly int _userID;
        public AdminForm(int userID)
        {
            InitializeComponent();
            _userService = new UserService(); 
            LoadUsers();
            RegisterEvents();
            this.Icon = new Icon(Application.StartupPath + @"\Resources\icon.ico");
            _userID = userID;
            this.AcceptButton = btnSearch;
        }

        private void LoadUsers()
        {
            foreach (DataGridViewColumn column in dataGridViewUsers.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            }

            var users = _userService.GetAllUsers();
            dataGridViewUsers.DataSource = users;
          

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
            var selectedRow = dataGridViewUsers.SelectedRows[0];
            string fullName = selectedRow.Cells["ФИО"].Value.ToString();
            using (var form = new UserEditForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadUsers();

                    AdminAuditLogger.LogToDb("Добавление пользователя",_userID,$"Добавлен пользователь {fullName}" );
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
            string userLogin = selectedRow.Cells["Логин"].Value.ToString();
            string fullName = selectedRow.Cells["ФИО"].Value.ToString();
            string Email = selectedRow.Cells["Email"].Value.ToString();
            string CreatedAt = selectedRow.Cells["Дата регистрации"].Value.ToString();
            var user = _userService.GetUserByLogin(userLogin);

            using (var form = new UserEditForm(user,fullName,Email,CreatedAt))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadUsers();
                    AdminAuditLogger.LogToDb("Изменение пользователя", _userID, $"Изменил пользователя {fullName}");
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

            int userId = Convert.ToInt32(dataGridViewUsers.SelectedRows[0].Cells["ID"].Value);
            var selectedRow = dataGridViewUsers.SelectedRows[0];
             string fullName = selectedRow.Cells["ФИО"].Value.ToString();
            var reasons = _userService.GetBanReasons();
            using (var form = new BanReasonForm(reasons))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    _userService.BanUser(userId, form.SelectedBanReasonId, form.AdminNote);
                    AdminAuditLogger.LogToDb("Блокировка пользователя", _userID, $"Заблокировал пользователя {fullName}");
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

            int id =Convert.ToInt32(dataGridViewUsers.SelectedRows[0].Cells["ID"].Value);
            var selectedRow = dataGridViewUsers.SelectedRows[0];
            string fullName = selectedRow.Cells["ФИО"].Value.ToString();
            try
            {
                _userService.DeleteUserByID(id);
                LoadUsers();
                MessageBox.Show("Пользователь успешно удалён.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                AdminAuditLogger.LogToDb("Удаление пользователя", _userID, $"Был удален пользователь {fullName}");
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
            var selectedRow = dataGridViewUsers.SelectedRows[0];
            string fullName = selectedRow.Cells["ФИО"].Value.ToString();
            var userId = Convert.ToInt32(dataGridViewUsers.SelectedRows[0].Cells["ID"].Value);
            _userService.SetUserBanStatus(userId, false);
            AdminAuditLogger.LogToDb("Снятие блокировки", _userID, $"Разблокировка пользователя {fullName}");
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

        private void button1_Click(object sender, EventArgs e)
        {
            AddBannedSoftwareForm addBannedSoftwareForm = new AddBannedSoftwareForm();
            addBannedSoftwareForm.ShowDialog();
        }
    }
}
