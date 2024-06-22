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
                string query = "SELECT UserID, PasswordHash, Role FROM Users WHERE Username = @Username";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    string storedHash = reader["PasswordHash"].ToString();
                    int userId = (int)reader["UserID"];
                    string role = reader["Role"].ToString();

                    if (PasswordHelper.VerifyPassword(password, storedHash))
                    {
                        SessionManager.StartSession(userId, username, role);
                        return true;
                    }
                }

                return false;
            }
        }

    }
}
