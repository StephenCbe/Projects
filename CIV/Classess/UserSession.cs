using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIV.Classess
{
    public class UserSession
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public bool IsAuthenticated { get; set; }
    }

}
