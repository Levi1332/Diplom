using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Diplom.Security;
using Diplom.Core.Service;
using System.Drawing;

namespace Diplom.UI
{
    public partial class UserEditForm : Form
    {
        public LoginResult UserModel;
        private readonly bool _isEditMode;
        private string _salt;
        private string _passwordHash;
        private static string connectionString = DatabaseConfig.connectionString;
        IUserService userService = new UserService();

        public UserEditForm()
        {
            InitializeComponent();
            _isEditMode = false;
            textBoxCreatedAt.Visible = false;
            labelCreatedAt.Visible = false;
            this.Icon = new Icon(Application.StartupPath + @"\Resources\icon.ico");
        }

        public UserEditForm(LoginResult user, string name, string email, string dataCreate)
        {
            InitializeComponent();
            _isEditMode = true;

            textBoxLogin.Text = user.Login;
            textBoxFullName.Text = name;
            textBoxEmail.Text = email;
            textBoxCreatedAt.Text = dataCreate;
            checkBoxIsBanned.Checked = user.IsBanned;

            textBoxLogin.Enabled = false; 
            textBoxPassword.Enabled = false;

            UserModel = user;
        }
          
        private void btnGeneratePassword_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxPassword.Text))
            {
                MessageBox.Show("Введите пароль перед генерацией хеша.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var encryption = new Encryption();
            _salt = encryption.GenerateSalt();
            _passwordHash = encryption.HashPassword(textBoxPassword.Text, _salt);

            MessageBox.Show("Хеш и соль успешно сгенерированы.", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            string login = textBoxLogin.Text;
            string fullName = textBoxFullName.Text;
            string Email = textBoxEmail.Text;
            bool isBaned = checkBoxIsBanned.Checked;
            if (!userService.ValidateFields(login,fullName,Email, _passwordHash, _salt, _isEditMode)) return;
            userService.SaveUserToDatabase(login, fullName, Email,_passwordHash,_salt,_isEditMode,isBaned );
            FinalizeSave();
        }

        private void FinalizeSave()
        {
            UserModel = new LoginResult
            {
                Login = textBoxLogin.Text,
                Password = textBoxPassword.Text,
                IsBanned = checkBoxIsBanned.Checked
            };

            MessageBox.Show(_isEditMode ? "Пользователь обновлён!" : "Пользователь успешно добавлен!", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}

