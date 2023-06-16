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
using System.IO;

namespace incomeAndExpenseAccounting
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Window
    {
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

        public int roleId;
        // Authorization to enter the app
        private bool Authorization(string email, string password)
        {
            // Searching for a user in the db
            var user = AppData.db.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
            if (user != null)
            {
                roleId = user.RoleId;
                string filePath = "C://Temp//incomeAndExpense/cookies.txt";
                File.WriteAllText(filePath, user.UsersId.ToString());
                return true;
            }
            else { return false; }
        }

        private bool IsAdmin(int roleId)
        {
            if (roleId == 1) { return true; } else { return false; }
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
                bool isAdmin = IsAdmin(roleId);

                if (result)
                {
                    if (isAdmin)
                    {
                        AdminPanel adminPanel = new AdminPanel();
                        adminPanel.Show();

                    }
                    else {
                        MainWindow mainWindow = new MainWindow();
                        mainWindow.Show();
                    }

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
