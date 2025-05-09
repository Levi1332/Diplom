using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Diplom
{
    internal class Encryption
    {
        public string GenerateSalt()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] saltBytes = new byte[16];
                rng.GetBytes(saltBytes);
                return Convert.ToBase64String(saltBytes);
            }
        }

        public string HashPassword(string password, string salt)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] saltBytes = Convert.FromBase64String(salt);
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] combinedBytes = passwordBytes.Concat(saltBytes).ToArray();
                byte[] hashBytes = sha256.ComputeHash(combinedBytes);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }

        public bool VerifyPassword(string inputPassword, string storedHash, string storedSalt)
        {
            string hashedInput = HashPassword(inputPassword, storedSalt);
            return hashedInput == storedHash;
        }
    }
}
