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
using System.IO;

namespace incomeAndExpenseAccounting
{
    /// <summary>
    /// Логика взаимодействия для EditRoles.xaml
    /// </summary>
    public partial class EditRoles : Window
    {
        public ObservableCollection<Roles> Roles { get; set; }
        public EditRoles()
        {
            InitializeComponent();
            Roles = new ObservableCollection<Roles>(AppData.db.Roles.ToList());
            // Установка контекста данных для привязки
            DataContext = this;
        }

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


        private void DeleteRole_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                // Получаем выделенную строку
                Roles selectedRole = (Roles)dataGrid.SelectedItem;
                int selectedRoleId = selectedRole.RoleId;
                // Удаляем выделенную строку из источника данных (например, из ObservableCollection)
                Roles.Remove(selectedRole);


                // Здесь вы можете выполнить необходимую логику удаления строки из базы данных
                ExecuteSql($"delete from Roles where RoleId = {selectedRoleId};");
                // Очищаем выделение в таблице
                dataGrid.SelectedItem = null;

            }
        }

        private void AddRole_Click(object sender, RoutedEventArgs e)
        {
            string newRoleName = txtNewRole.Text;

            if (!string.IsNullOrEmpty(newRoleName))
            {

                Roles newRole = new Roles()
                {
                    Name = newRoleName
                };

                Roles.Add(newRole);

                AppData.db.Roles.Add(newRole);
                AppData.db.SaveChanges();

                dataGrid.ItemsSource = null;
                dataGrid.ItemsSource = Roles;

                // Очистка TextBox
                txtNewRole.Text = string.Empty;

            }
            else
            {
                MessageBox.Show("Напишите название категории");
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = null;
            dataGrid.ItemsSource = Roles;
        }
    }
}
