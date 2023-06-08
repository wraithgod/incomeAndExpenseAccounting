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

namespace incomeAndExpenseAccounting
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>                                                                                                                                    
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void Update() { 
            var content = AppData.db.Expenses.ToList();
            ChecksList.ItemsSource = content;
        }

        private void AddIncome_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddExpense_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ExportData_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EditCategory_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UpdateClick(object sender, RoutedEventArgs e)
        {
            Update();
        }
    }
}
