using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для EditCategory.xaml
    /// </summary>
    public partial class EditCategory : Window
    {
        public ObservableCollection<Categories> Categories { get; set; }

        public EditCategory()
        {
            InitializeComponent();

            // Создание коллекции категорий и заполнение ее данными из базы данных
            Categories = new ObservableCollection<Categories>(AppData.db.Categories.ToList());

            // Установка контекста данных для привязки
            DataContext = this;
        }

        private void EditCategory_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteCategory_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddCategory_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
