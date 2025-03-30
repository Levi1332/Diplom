using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Diplom
{
    internal class CLogin
    {
        private static string connectionString = DatabaseConfig.connectionString;
        public bool Login(string login, string password)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT IsBanned, BanReasonID, PasswordHash, Salt FROM Users WHERE Login = @Login";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Login", login);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        MessageBox.Show("Неверный логин или пароль!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    Encryption encryption = new Encryption();
                    bool isBanned = reader.GetBoolean(0);
                    int? banReasonID = reader.IsDBNull(1) ? (int?)null : reader.GetInt32(1);
                    string storedPasswordHash = reader.GetString(2);
                    string salt = reader.GetString(3); 


                    if (!encryption.VerifyPassword(password, storedPasswordHash, salt))
                    {
                        MessageBox.Show("Неверный логин или пароль!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    if (isBanned && banReasonID.HasValue)
                    {
                        CBanInfo cBanInfo = new CBanInfo();
                        string banInfo = cBanInfo.GetBanInfo(banReasonID.Value);
                        MessageBox.Show($"Ваш аккаунт заблокирован!\n{banInfo}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }

            MessageBox.Show("Вход успешен!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return true;
        }      
    }
}
