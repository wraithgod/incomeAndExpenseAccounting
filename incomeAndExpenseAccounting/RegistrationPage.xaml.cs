using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace incomeAndExpenseAccounting
{
    /// <summary>
    /// Логика взаимодействия для RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Window
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }

        // Hyperlink redirect
        private void HyperLinkClick(object sender, RoutedEventArgs e)
        {
            LoginPage loginPage = new LoginPage();
            loginPage.Show();

            Window.GetWindow(this).Close();
        }


        private bool IsValidEmail(string email)
        {
            // Pattern for email format validation
            string pattern = @"^[^\s@]+@[^\s@]+\.[^\s@]+$";

            // Check using regular expression
            return Regex.IsMatch(email, pattern);
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Taking data from TextBoxes
            string lastName = txtLastName.Text;
            string firstName = txtFirstName.Text;
            string email = txtEmail.Text;
            string password = txtPassword.Password;
            string confirmPassword = txtConfirmPassword.Password;

            bool existsMail = true;

            // Check if email is valid
            bool isEmailValid = IsValidEmail(email);

            if (isEmailValid)
            {
                // Check if email already exists in the database
                existsMail = AppData.db.Users.Any(u => u.Email == email);
            }

            // Check if last name and first name are valid
            bool lastNameExists = !string.IsNullOrEmpty(lastName);
            bool firstNameExists = !string.IsNullOrEmpty(firstName);

            // Check if passsword is correct and same as confirm password
            bool samePass = password == confirmPassword;
            bool correctPass = password.Length >= 8;

            if (lastNameExists && firstNameExists && correctPass && !existsMail && samePass)
            {
                // Trying to add a user to the database
                try
                {
                    // Create a new instance of User and set its properties
                    Users newUser = new Users()
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        Email = email,
                        Password = password,
                        RoleId = 2,
                        Balance = 0,
                    };

                    // Add the new user to the Users table
                    AppData.db.Users.Add(newUser);

                    // Save changes to the database
                    AppData.db.SaveChanges();

                    // Success message output 
                    MessageBox.Show("Вы успешно зарегистировались");

                    // Closing registration page and redirecting to login page
                    LoginPage loginPage = new LoginPage();
                    loginPage.Show();


                    Window.GetWindow(this).Close();
                }
                catch (Exception ex)
                {
                    // Error message output
                    MessageBox.Show("Что-то пошло не так. Подробнее - " + ex);
                }
            }

            // Error when field with first name is empty
            else if (!firstNameExists)
            {
                MessageBox.Show("Введите имя");
            }

            // Error when field with last name is empty
            else if (!lastNameExists)
            {
                MessageBox.Show("Введите фамилию");
            }

            // Error when entering incorrect mail
            else if (!isEmailValid)
            {
                MessageBox.Show("Некорректо введена почта");
            }

            // Error when entering not the same passes
            else if (!samePass)
            {
                MessageBox.Show("Пароли не совпадают");
            }

            // Error when entering incorrect pass
            else if (!correctPass)
            {
                MessageBox.Show("Измените пароль. Пароль должен содержать не менее 8 символов");
            }

            // Error when email already used
            else if (existsMail)
            {
                MessageBox.Show("Пользователь с данной почтой уже зарегистрирован");
            }
        }
    }
}
