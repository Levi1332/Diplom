using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Diplom.Security;


namespace Diplom.UI
{
    public partial class UserEditForm : Form
    {
        public LoginResult UserModel;
        private readonly bool _isEditMode;
        private string _salt;
        private string _passwordHash;
        private static string connectionString = DatabaseConfig.connectionString;

        public UserEditForm()
        {
            InitializeComponent();
            _isEditMode = false;
            textBoxCreatedAt.Visible = false;
            labelCreatedAt.Visible = false;
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
            if (string.IsNullOrWhiteSpace(textBoxLogin.Text) ||
                string.IsNullOrWhiteSpace(textBoxFullName.Text) ||
                string.IsNullOrWhiteSpace(textBoxEmail.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все обязательные поля.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!_isEditMode && (string.IsNullOrWhiteSpace(_passwordHash) || string.IsNullOrWhiteSpace(_salt)))
            {
                MessageBox.Show("Сначала сгенерируйте хеш пароля.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command;

                if (_isEditMode)
                {
                    command = new SqlCommand(@"
                UPDATE Users 
                SET FullName = @FullName, Email = @Email, IsBanned = @IsBanned
                WHERE Login = @Login", connection);
                }
                else
                {
                    command = new SqlCommand(@"
                INSERT INTO Users (FullName, Login, Email, PasswordHash, Salt, CreatedAt, IsBanned)
                VALUES (@FullName, @Login, @Email, @PasswordHash, @Salt, @CreatedAt, @IsBanned)", connection);

                    command.Parameters.AddWithValue("@PasswordHash", _passwordHash);
                    command.Parameters.AddWithValue("@Salt", _salt);
                    command.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
                }

                command.Parameters.AddWithValue("@FullName", textBoxFullName.Text);
                command.Parameters.AddWithValue("@Login", textBoxLogin.Text);
                command.Parameters.AddWithValue("@Email", textBoxEmail.Text);
                command.Parameters.AddWithValue("@IsBanned", checkBoxIsBanned.Checked);

                command.ExecuteNonQuery();
            }

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

