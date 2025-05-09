using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Security
{
    public class LoginResult
    {
        public bool Success { get; set; }
        public string Role { get; set; }
        public int UserId { get; set; }
        public string ErrorMessage { get; set; }
        public string Login { get; set; }
        public bool IsBanned { get; set; }
        public string Password { get; set; }

    }

}
