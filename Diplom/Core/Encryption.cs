using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

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
        public bool VerifyPassword(string inputPassword, string storedHash, string storedSalt)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] saltBytes = Convert.FromBase64String(storedSalt);
              
                byte[] passwordBytes = Encoding.UTF8.GetBytes(inputPassword);
               
                byte[] combinedBytes = passwordBytes.Concat(saltBytes).ToArray();
                
                byte[] hashBytes = sha256.ComputeHash(combinedBytes);
              
                string hashedInput = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

                
                return hashedInput == storedHash;
            }
        }

    }
}
