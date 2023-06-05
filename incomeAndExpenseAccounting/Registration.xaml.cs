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
using System.Text.RegularExpressions;


namespace incomeAndExpenseAccounting
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Page
    {
        public Registration()
        {
            InitializeComponent();
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
            //Taking data from TextBoxes
            string lastName = txtLastName.Text;
            string firstName = txtFirstName.Text;
            string email = txtEmail.Text;
            string password = txtPassword.Password;
            string confirmPassword = txtConfirmPassword.Password;

            
        }



    }
}
