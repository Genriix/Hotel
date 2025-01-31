using Hotel.Classes;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hotel.Pages
{
    /// <summary>
    /// Логика взаимодействия для ClientPage.xaml
    /// </summary>
    public partial class ClientPage : Page
    {
        public ClientPage()
        {
            InitializeComponent();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            AddNewPassword(OldPassword.Text, NewPassword.Text, RepetePassword.Text);
        }

        private static void AddNewPassword(string oldPassword, string newPassword, string repPassword)
        {
            User.errors.Clear();
            using (SqlConnection connection = new SqlConnection(Manager.connectionString))
            {
                connection.Open();

                string query = "SELECT Password FROM [User] WHERE id = @id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", User.GetUser_id());

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            if (oldPassword != reader.GetString(0))
                            {
                                User.errors.AppendLine("Пароль не правильный");
                            }
                        }
                    }
                }

                if (newPassword != repPassword)
                {
                    User.errors.AppendLine("Пароли не совпадают");
                }

                if (User.errors.Length == 0)
                {
                    query = "UPDATE [User] SET Password = @NewPassword WHERE id = @id";

                    using (SqlCommand command = new SqlCommand(query, connection)) // Наполняем комманду переменными
                    {
                        command.Parameters.AddWithValue("@NewPassword", newPassword); // Вместо ЮсерТайпИд пишем 1
                        command.Parameters.AddWithValue("@id", User.GetUser_id()); // Вместо Логин пишем то, что в Текстбоксе, и так далее

                        int rowsAffected = command.ExecuteNonQuery();
                    }
                    MessageBox.Show("Пароль сменён!", "Успех!");
                }
                else
                {
                    MessageBox.Show(User.errors.ToString(), "Ошибка смены пароля");
                }
            }
        }
    }
}
