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
using System.Collections.ObjectModel;

namespace incomeAndExpenseAccounting
{
    /// <summary>
    /// Логика взаимодействия для SendMessage.xaml
    /// </summary>
    public partial class SendMessage : Window
    {
        public ObservableCollection<Messages> Messages { get; set; }
        private int GetCurrentUserId()
        {
            string filePath = "C://Temp/incomeAndExpense/cookies.txt";
            string usersId = File.ReadAllText(filePath);
            return Int32.Parse(usersId);
        }

        public SendMessage()
        {
            InitializeComponent();
            Messages = new ObservableCollection<Messages>();
        }

        private void SendMessage_Click(object sender, RoutedEventArgs e)
        {
            string newMessageString = messageBox.Text;

            if (!string.IsNullOrEmpty(newMessageString))
            {

                Messages newMessage = new Messages()
                {
                    Message = newMessageString,
                    UsersId = GetCurrentUserId()
                };

                Messages.Add(newMessage);

                AppData.db.Messages.Add(newMessage);
                AppData.db.SaveChanges();

                messageBox.Text = string.Empty;
                MessageBox.Show("Сообщение успешно отправлено!");
                

            }
            else
            {
                MessageBox.Show("Напишите сообщение администратору!");
            }
        }
    }
}
