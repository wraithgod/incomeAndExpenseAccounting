﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Data;
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
using Microsoft.Win32;
using System.IO;


namespace incomeAndExpenseAccounting
{
    /// <summary>
    /// Логика взаимодействия для EditCategory.xaml
    /// </summary>
    public partial class EditCategory : Window
    {
        public ObservableCollection<Categories> Categories { get; set; }

        private int GetCurrentUserId()
        {
            string filePath = "C://Temp/incomeAndExpense/cookies.txt";
            string usersId = File.ReadAllText(filePath);
            return Int32.Parse(usersId);
        }

        static DataTable ExecuteSql(string sql)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection conn = new SqlConnection(
                    "Data Source=ROFLAN;Integrated Security=True;Initial Catalog=kursa4"
                    );

                using (conn)
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader read = cmd.ExecuteReader();

                    using (read)
                    {
                        dt.Load(read);
                    }
                }

                return dt;
            }
            catch {
                MessageBox.Show("Нельзя удалить данную категорию пока с ней имеется запись");
            }
            return null;
        }

        public EditCategory()
        {
            InitializeComponent();

            // Создание коллекции категорий и заполнение ее данными из базы данных
            Categories = new ObservableCollection<Categories>(AppData.db.Categories.ToList());
            // Установка контекста данных для привязки
            DataContext = this;
        }


        private void DeleteCategory_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                // Получаем выделенную строку
                Categories selectedCategory = (Categories)dataGrid.SelectedItem;
                int selectedCategoryId = selectedCategory.CategoryId;
                // Удаляем выделенную строку из источника данных (например, из ObservableCollection)
                Categories.Remove(selectedCategory);

                // Здесь вы можете выполнить необходимую логику удаления строки из базы данных
                ExecuteSql($"delete from Categories where CategoryId = {selectedCategoryId};");
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
