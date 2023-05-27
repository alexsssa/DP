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

namespace DP
{
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        string user = "admin";   //Из БД
        DateTime date = DateTime.Today;
        public Main()
        {
            InitializeComponent();
            init();


        }

        private void init()
        {
            username.Content = user.ToString();
            string day = date.ToLongDateString();
            currentdate.Content = day.Substring(0, day.Length - 7);
        }

        private void button_exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void lastday_Click(object sender, RoutedEventArgs e)
        {
            date = date.AddDays(-1);
            init();
        }

        private void nextday_Click(object sender, RoutedEventArgs e)
        {
            date = date.AddDays(1);
            init();
        }

        private void button_addtask(object sender, RoutedEventArgs e)
        {
            // Добавление новой записи
        }

        private void button_deltask(object sender, RoutedEventArgs e)
        {
            // Удаление записи
        }

        private void button_donetask(object sender, RoutedEventArgs e)
        {
            // Переключение чекбокса записи
        }

        private void ex_account(object sender, RoutedEventArgs e)
        {
            ((Autorization)Application.Current.MainWindow).Show();
            this.Close();
        }
    }
}
