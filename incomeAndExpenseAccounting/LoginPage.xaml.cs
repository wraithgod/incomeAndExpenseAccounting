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
using System.Windows.Shapes;

namespace incomeAndExpenseAccounting
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Window
    {
        public int userId = 0;
        public LoginPage()
        {
            InitializeComponent();
        }

        // Hyperlink redirect
        private void HyperLinkClick(object sender, RoutedEventArgs e)
        {
            RegistrationPage registrationPage = new RegistrationPage();
            registrationPage.Show();

            Window.GetWindow(this).Close();
        }

        // Authorization to enter the app
        private bool Authorization(string email, string password)
        {
            // Searching for a user in the db
            var user = AppData.db.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
            userId = user.UsersId;
            if (user != null) { return true; } else { return false; }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Taking data from TextBoxes
            string email = txtEmail.Text;
            string pass = txtPassword.Password;

            // Trying to login
            try
            {
                bool result = Authorization(email, pass);

                if (result)
                {
                    // Closing login page and redirecting to main page
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();


                    Window.GetWindow(this).Close();

                }
                else
                {
                    MessageBox.Show("Введенные данные неверны либо пользователь не зарегистрирован");
                }
            }
            catch
            {
                MessageBox.Show("Что-то пошло не так.");
            };
        }
    }
}
