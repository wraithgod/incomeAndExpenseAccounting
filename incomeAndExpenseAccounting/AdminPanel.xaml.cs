using CsvHelper;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
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
    /// Логика взаимодействия для AdminPanel.xaml
    /// </summary>
    public partial class AdminPanel : Window
    {
        public ObservableCollection<Expenses> Expenses { get; set; }
        public AdminPanel()
        {
            InitializeComponent();
            Expenses = new ObservableCollection<Expenses>(AppData.db.Expenses.ToList());
            // Установка контекста данных для привязки
            DataContext = this;
        }

        static DataTable ExecuteSql(string sql)
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

        private int GetCurrentUserId()
        {
            string filePath = "C://Temp/incomeAndExpense/cookies.txt";
            string usersId = File.ReadAllText(filePath);
            return Int32.Parse(usersId);
        }


        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void EditCategory_Click(object sender, RoutedEventArgs e)
        {
            EditCategory editCategory = new EditCategory();
            editCategory.Show();
        }

        private void EditUsers_Click(object sender, RoutedEventArgs e)
        {
            UsersWindow editUsers = new UsersWindow();
            editUsers.Show();
        }

        private void AddRecord_Click(object sender, RoutedEventArgs e)
        {
            AddRecord addRecord = new AddRecord();
            addRecord.Show();
        }


        private void ExportData_Click(object sender, RoutedEventArgs e)
        {
            int userId = GetCurrentUserId();
            DataTable dataTable = ExecuteSql("SELECT Expenses.Description, Categories.Name, Expenses.Amount, Expenses.Date " +
                        "FROM Expenses " +
                        "INNER JOIN Categories ON Expenses.CategoryId = Categories.CategoryId " +
                        "where UserId = " + userId);


            if (dataTable.Rows.Count > 0)
            {


                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "CSV Files (*.csv)|*.csv";

                if (saveFileDialog.ShowDialog() == true)
                {
                    using (StreamWriter sw = new StreamWriter(saveFileDialog.FileName, false, Encoding.UTF8))
                    using (CsvWriter csvWriter = new CsvWriter(sw, CultureInfo.InvariantCulture, leaveOpen: true))


                    {
                        // Записать заголовки столбцов
                        foreach (DataColumn column in dataTable.Columns)
                        {
                            csvWriter.WriteField(column.ColumnName);
                        }
                        csvWriter.NextRecord();

                        // Записать данные
                        foreach (DataRow row in dataTable.Rows)
                        {
                            foreach (object item in row.ItemArray)
                            {
                                csvWriter.WriteField(item);
                            }
                            csvWriter.NextRecord();
                        }
                        MessageBox.Show("Данные успешно экспортированы в файл CSV.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Нет данных для экспорта.");
            }
        }

        private void UpdateClick(object sender, RoutedEventArgs e)
        {

        }

        private void EditRoles_Click(object sender, RoutedEventArgs e)
        {
            EditRoles editRoles = new EditRoles();
            editRoles.Show();
        }

        private void DeleteSelected_Click(object sender, RoutedEventArgs e)
        {
            if (ChecksList.SelectedItem != null)
            {
                // Получаем выделенную строку
                Expenses selectedExpense = (Expenses)ChecksList.SelectedItem;
                int selectedExpenseId = selectedExpense.ExpenseId;
                // Удаляем выделенную строку из источника данных (например, из ObservableCollection)
                Expenses.Remove(selectedExpense);

                // Здесь вы можете выполнить необходимую логику удаления строки из базы данных
                ExecuteSql($"delete from Expenses where ExpenseId = {selectedExpenseId};");
                // Очищаем выделение в таблице
                ChecksList.SelectedItem = null;

            }
        }

        private void viewMessages_Click(object sender, RoutedEventArgs e)
        {
            RecieveMessage recieveMessage = new RecieveMessage();
            recieveMessage.Show();
        }
    }
}
