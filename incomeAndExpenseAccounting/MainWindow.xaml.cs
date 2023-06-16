using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;
using Microsoft.Win32;
using System.Collections;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.Data.Entity.Infrastructure;

namespace incomeAndExpenseAccounting
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>                                                                                                                                    
    public partial class MainWindow : Window
    {

        public ObservableCollection<Expenses> Expenses { get; set; }
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



        public MainWindow()
        {
            InitializeComponent();
            Update();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            int userId = GetCurrentUserId();
            //ChecksList.ItemsSource = ;
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = (TextBlock)sender;
            string filePath = textBlock.Text;

            // ...
        }

        public void Update() {
            int userId = GetCurrentUserId();
            DataTable dt = ExecuteSql("SELECT Expenses.Description, Categories.Name, Expenses.Amount, Expenses.Date " +
                                        "FROM Expenses " +
                                        "INNER JOIN Categories ON Expenses.CategoryId = Categories.CategoryId " +
                                        "where UserId = " + userId);
            ChecksList.ItemsSource = dt.DefaultView;
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
            Update();
        }

        private void SendMessage_Click(object sender, RoutedEventArgs e)
        {
            SendMessage sendMessage = new SendMessage();
            sendMessage.Show();
        }
    }
}
