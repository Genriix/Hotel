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
using System.Xml.Linq;

namespace Hotel.Pages
{
    /// <summary>
    /// Логика взаимодействия для AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        public AdminPage()
        {
            InitializeComponent();
        }

        private void ButtonReg_Click(object sender, RoutedEventArgs e)
        {
            if (IsAdmin.IsChecked == true)
            {
                AddNewUser(1,Login.Text, Password.Text, FullName.Text, Phone.Text);
            }
            else
            {
                AddNewUser(2, Login.Text, Password.Text, FullName.Text, Phone.Text);
            }
            Login.Text = "";
            Password.Text = "";
            FullName.Text = "";
            Phone.Text = "";
        }
        private static void AddNewUser(int role_id, string login, string password, string fullName, string telephoneNumber)
        {
            using (SqlConnection connection = new SqlConnection(Manager.connectionString))
            {
                connection.Open();

                string query = "" +
                    "INSERT INTO [User] (Role_id, Login, Password, FullName, TelephoneNumber) " +
                    "VALUES (@Role_id, @Login, @Password, @FullName, @TelephoneNumber)";

                using (SqlCommand command = new SqlCommand(query, connection)) // Наполняем комманду переменными
                {
                    command.Parameters.AddWithValue("@Role_id", role_id); // Вместо ЮсерТайпИд пишем 1
                    command.Parameters.AddWithValue("@Login", login); // Вместо Логин пишем то, что в Текстбоксе, и так далее
                    command.Parameters.AddWithValue("@Password", password);
                    command.Parameters.AddWithValue("@FullName", fullName);
                    command.Parameters.AddWithValue("@TelephoneNumber", telephoneNumber);

                    // Выполнение команды
                    int rowsAffected = command.ExecuteNonQuery();
                }
                MessageBox.Show("Добавлен новый пользователь в базу данных!", "Успех!");
            }
        }
    }
}
