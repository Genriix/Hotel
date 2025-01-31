using Hotel.Classes;
using Hotel.Pages;
using System;
using System.Collections.Generic;
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

namespace Hotel
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {

        public LoginPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            User.FoundUser(Login.Text, Password.Password.ToString());
            if (LoginPage.IsErrorsEmpty())
            {
                switch (User.GetUserRole_id())
                {
                    case 1: Manager.MainFrame.Navigate(new AdminPage()); break;
                    case 2: Manager.MainFrame.Navigate(new ClientPage()); break;
                }
                Login.Text = "";
                Password.Password = "";
            }
            else
            {
                MessageBox.Show(User.errors.ToString(), "Ошибка входа");
            }
            User.errors.Clear();
        }

        private static bool IsErrorsEmpty()
        {
            return User.errors.Length == 0;
        }
    }
}
