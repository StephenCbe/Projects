using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIV.Classess
{
    public static class SessionManager
    {
        public static UserSession CurrentUser { get; private set; }

        public static void StartSession(int userId, string username, string role)
        {
            CurrentUser = new UserSession
            {
                UserId = userId,
                Username = username,
                Role = role,
                IsAuthenticated = true
            };
        }

        public static void EndSession()
        {
            CurrentUser = null;
        }
    }

}
