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
    /// Логика взаимодействия для UsersWindow.xaml
    /// </summary>
    public partial class UsersWindow : Window
    {

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
        public ObservableCollection<Users> Users { get; set; }
        public UsersWindow()
        {
            InitializeComponent();
            Users = new ObservableCollection<Users>(AppData.db.Users.ToList());
            // Установка контекста данных для привязки
            DataContext = this;
        }


        private void Update_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = null;
            dataGrid.ItemsSource = Users;
        }

        private void DeleteUser_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                // Получаем выделенную строку
                Users selectedUser = (Users)dataGrid.SelectedItem;
                int selectedUserId = selectedUser.UsersId;
                // Удаляем выделенную строку из источника данных (например, из ObservableCollection)
                Users.Remove(selectedUser);

                // Здесь вы можете выполнить необходимую логику удаления строки из базы данных
                ExecuteSql($"delete from Users where UsersId = {selectedUserId};");
                // Очищаем выделение в таблице
                dataGrid.SelectedItem = null;

            }
        }
    }
}
