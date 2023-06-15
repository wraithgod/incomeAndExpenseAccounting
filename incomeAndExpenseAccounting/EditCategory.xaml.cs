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
            if (dataGrid.SelectedItem != null)
            {
                // Получаем выделенную строку
                Categories selectedCategory = (Categories)dataGrid.SelectedItem;

                // Удаляем выделенную строку из источника данных (например, из ObservableCollection)
                Categories.Remove(selectedCategory);

                // Здесь вы можете выполнить необходимую логику удаления строки из базы данных

                // Очищаем выделение в таблице
                dataGrid.SelectedItem = null;
            }
        }

        private void AddCategory_Click(object sender, RoutedEventArgs e)
        {
            string newCategoryName = txtNewCategory.Text;

            if (!string.IsNullOrEmpty(newCategoryName))
            {

                Categories newCategory = new Categories()
                {
                    Name = newCategoryName
                };

                Categories.Add(newCategory);

                AppData.db.Categories.Add(newCategory);
                AppData.db.SaveChanges();

                dataGrid.ItemsSource = null;
                dataGrid.ItemsSource = Categories;

                // Очистка TextBox
                txtNewCategory.Text = string.Empty;

            }
            else {
                MessageBox.Show("Напишите название категории");
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = null;
            dataGrid.ItemsSource = Categories;
        }
    }
}
