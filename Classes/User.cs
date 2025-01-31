using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Classes
{
    internal class User
    {
        private static int user_id;
        private static int user_role_id;
        public static StringBuilder errors = new StringBuilder();


        public static int GetUser_id()
        {
            return user_id;
        }
        public static int GetUserRole_id()
        {
            return user_role_id;
        }

        public static void FoundUser(string login, string password)
        {
            using (SqlConnection connection = new SqlConnection(Manager.connectionString))
            {
                int founded_id = 0;
                int founded_role = 0;
                connection.Open();

                string query = "SELECT id, Role_id FROM [User] WHERE Login = @Login";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Login", login);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            founded_id = reader.GetInt32(0); // Получаем значение столбца id
                            founded_role = reader.GetInt32(1);
                        }
                        else
                        {
                            errors.AppendLine("Пользователь не найден");
                        }
                    }
                }

                query = "SELECT Password FROM [User] WHERE id = @id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", founded_id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            if (password == reader.GetString(0))
                            {
                                user_id = founded_id;
                                user_role_id = founded_role;
                            }
                            else
                            {
                                errors.AppendLine("Пароль не правильный");
                            }
                        }
                    }
                }
            }
        }
    }
}
