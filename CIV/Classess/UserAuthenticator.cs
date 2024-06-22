using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIV.Classess
{
    internal class UserAuthenticator
    {
        private string connectionString = GlobalFn.GetConnString;

        public void CreateUser(string username, string password, string email, string role)
        {
            string hashedPassword = PasswordHelper.HashPassword(password);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Users (Username, PasswordHash, Email, Role) VALUES (@Username, @PasswordHash, @Email, @Role)";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@PasswordHash", hashedPassword);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Role", role);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public bool ValidateUser(string username, string password)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT PasswordHash FROM Users WHERE Username = @Username";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);

                connection.Open();
                string storedHash = command.ExecuteScalar() as string;

                if (storedHash == null)
                {
                    return false; // User not found
                }

                return PasswordHelper.VerifyPassword(password, storedHash);
            }
        }
    }
}
