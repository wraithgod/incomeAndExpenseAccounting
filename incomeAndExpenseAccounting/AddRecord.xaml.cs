using Microsoft.Win32;
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
    /// Логика взаимодействия для AddRecord.xaml
    /// </summary>
    public partial class AddRecord : Window
    {
        string filePath;

        

        public AddRecord()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                filePath = openFileDialog.FileName;
                if (string.IsNullOrEmpty(filePath)) {
                    byte[] fileContent = File.ReadAllBytes(filePath);
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // Получение значений из полей формы
            string description = txtDescription.Text;
            Categories category = (Categories)txtNewCategory.SelectedItem; // Предполагается, что выбран объект типа Category из ComboBox
            string reciept = filePath; // Предполагается, что путь к файлу сохранен в переменной filePath
            decimal amount = decimal.Parse(summ.Text);
            DateTime date = datePicker.SelectedDate ?? DateTime.Now;

            LoginPage loginPage = new LoginPage();

            // Создание нового объекта Expenses
            Expenses expenses = new Expenses
            {
                Description = description,
                Categories = category,
                Reciept = Encoding.UTF8.GetBytes(reciept), // Преобразование строки пути к файлу в массив байтов
                Amount = amount,
                Date = date,
                UserId = loginPage.userId
            };

            AppData.db.Expenses.Add(expenses);
            AppData.db.SaveChanges();

            // Очистка полей формы после добавления записи
            txtDescription.Text = string.Empty;
            txtNewCategory.SelectedItem = null;
            filePath = string.Empty; // Очистка переменной filePath или другой переменной, содержащей путь к файлу
            summ.Text = string.Empty;
            datePicker.SelectedDate = DateTime.Now;
        }
    }
}
