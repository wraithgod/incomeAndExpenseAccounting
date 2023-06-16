using System;
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

namespace incomeAndExpenseAccounting
{
    /// <summary>
    /// Логика взаимодействия для RecieveMessage.xaml
    /// </summary>
    public partial class RecieveMessage : Window
    {
        public RecieveMessage()
        {
            InitializeComponent();
            Messages = new ObservableCollection<Messages>(AppData.db.Messages.ToList());
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
        public ObservableCollection<Messages> Messages { get; set; }


        private void Update_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = null;
            dataGrid.ItemsSource = Messages;
        }


        private void DeleteMessage_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                // Получаем выделенную строку
                Messages selectedMessage = (Messages)dataGrid.SelectedItem;
                int selectedMessageId = selectedMessage.MessagesId;

                Messages.Remove(selectedMessage);

                ExecuteSql($"delete from Messages where MessagesId = {selectedMessageId};");
                // Очищаем выделение в таблице
                dataGrid.SelectedItem = null;

            }
        }
    }
}
