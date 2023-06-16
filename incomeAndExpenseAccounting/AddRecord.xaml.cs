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
using System.Data;
using System.Data.SqlClient;

namespace incomeAndExpenseAccounting
{
    /// <summary>
    /// Логика взаимодействия для AddRecord.xaml
    /// </summary>
    public partial class AddRecord : Window
    {
        private int GetCurrentUserId()
        {
            string filePath = "C://Temp/incomeAndExpense/cookies.txt";
            string usersId = File.ReadAllText(filePath);
            return Int32.Parse(usersId);
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

        public AddRecord()
        {
            InitializeComponent();
            DataTable dataTable = ExecuteSql("SELECT * FROM Categories");
            txtNewCategory.ItemsSource = dataTable.DefaultView;
            txtNewCategory.DisplayMemberPath = "Name";
        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try {
                // Получение значений из полей формы
                string description = txtDescription.Text;
                DataRowView selectedRow = txtNewCategory.SelectedItem as DataRowView;
                string selectedName = selectedRow["Name"].ToString();
                int selectedId = (int)selectedRow["CategoryId"];
                decimal amount = decimal.Parse(summ.Text);
                DateTime date = datePicker.SelectedDate ?? DateTime.Now;
                int userid = GetCurrentUserId();
                try{
                    ExecuteSql($"insert into Expenses (UserId, Amount, Date, CategoryId, Description) values ({userid},{amount},{date},{selectedId},{description});");
                    MessageBox.Show("Данные успешно добавлены!");
                } catch {
                    MessageBox.Show("Ошибка запроса");
                }
                

                // Очистка полей формы после добавления записи
                txtDescription.Text = string.Empty;
                txtNewCategory.SelectedItem = null;
                summ.Text = string.Empty;
                datePicker.SelectedDate = DateTime.Now;
            }
            catch {
                MessageBox.Show("Где-то ошибка. Попробуй перепроверить данные");
            }
        }
    }
}
