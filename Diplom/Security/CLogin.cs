using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using Diplom.Security;

namespace Diplom
{
    internal class CLogin
    {
        private WorkTimeSettings _settings;
        private static string connectionString = DatabaseConfig.connectionString;
        public LoginResult Login(string login, string password)
        {
            var result = new LoginResult();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT UserID, IsBanned, BanReasonID, PasswordHash, Salt, Role FROM Users WHERE Login = @Login";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Login", login);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        result.Success = false;
                        result.ErrorMessage = "Неверный логин или пароль!";
                        return result;
                    }

                    Encryption encryption = new Encryption();
                    int userId = reader.GetInt32(0);
                    bool isBanned = reader.GetBoolean(1);
                    int? banReasonID = reader.IsDBNull(2) ? (int?)null : reader.GetInt32(2);
                    string storedPasswordHash = reader.GetString(3);
                    string salt = reader.GetString(4);
                    string role = reader.GetString(5);

                    if (!encryption.VerifyPassword(password, storedPasswordHash, salt))
                    {
                        result.Success = false;
                        result.ErrorMessage = "Неверный логин или пароль!";
                        return result;
                    }

                    if (isBanned && banReasonID.HasValue)
                    {
                        CBanInfo cBanInfo = new CBanInfo();
                        string banInfo = cBanInfo.GetBanInfo(banReasonID.Value);
                        result.Success = false;
                        result.ErrorMessage = $"Ваш аккаунт заблокирован!\n{banInfo}";
                        return result;
                    }

                    result.Success = true;
                    result.Role = role;
                    result.UserId = userId;
                }
            }

            _settings = SettingsManager.LoadSettings();
            if (_settings.NotifyLogin)
            {
                MessageBox.Show("Вход успешен!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return result;
        }

    }
}
