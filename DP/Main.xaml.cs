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
using System.Data.SqlClient;
using System.Data.SQLite;
using System.IO;
using System.Data;

namespace DP
{
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        string user = "";   //Из БД
        int user_id = -1;
        DateTime date = DateTime.Today;

 

        Event selected_ev;

        public Main(int id, string input_user)
        {
            InitializeComponent();
            user_id = id;
            user = input_user;
            init();
        }

        private void init()
        {
            username.Content = user.ToString();
            string day = date.ToLongDateString();
            currentdate.Content = day.Substring(0, day.Length - 8);


            string query = $"select * from events where id_user = '{Convert.ToInt32(user_id)}' and Date = '{currentdate.Content}'";
            
            SQLiteConnection sqlite = new SQLiteConnection("Data Source=database.db; Version=3;");

            using (sqlite)
            {
                SQLiteCommand sqlcmd;
                sqlite.Open();
                sqlcmd = sqlite.CreateCommand();
                sqlcmd.CommandText = query;
                SQLiteDataReader SQL = sqlcmd.ExecuteReader();

                List<string> name_events = new List<string>();
                List<Event> events = new List<Event>();

                if (SQL.HasRows)
                {
                    
                    while (SQL.Read())
                    {
                        Event ev = new Event(Convert.ToInt32(SQL["ID"]), SQL["Name"].ToString(), SQL["Date"].ToString(), SQL["Time"].ToString(), SQL["Other"].ToString());

                        name_events.Add(ev.name);

                        events.Add(ev);
                    }
                }
                table.ItemsSource = name_events;

                if (this.table.SelectedItems.Count != 0)
                {
                    string selected_name = ItemName.Content.ToString();

                    selected_ev = events[name_events.IndexOf(selected_name)];

                    ItemDate.Content = $"Дата: {selected_ev.date}";
                    ItemTime.Content = $"Время: {selected_ev.time}";
                    ItemText.Content = selected_ev.other;
                }

                sqlite.Close();
            }
        }

        private void button_exit(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
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
            Question question = new Question();
            question.ShowDialog();

            if (question.flag)
            {
                if (this.table.SelectedItems.Count != 0)
                {

                    string query = $"delete from events where ID='{Convert.ToInt32(selected_ev.id)}'";

                    SQLiteConnection sqlite = new SQLiteConnection("Data Source=database.db; Version=3;");

                    using (sqlite)
                    {
                        SQLiteCommand sqlcmd;
                        sqlite.Open();
                        sqlcmd = sqlite.CreateCommand();
                        sqlcmd.CommandText = query;
                        sqlcmd.ExecuteNonQuery();
                        sqlite.Close();
                    }
                }
            }
            init();
        }

        private void ex_account(object sender, RoutedEventArgs e)
        {
            new Autorization().Show();
            this.Close();
        }

        private void table_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ItemDate.Content = "Дата:";
            ItemTime.Content = "Время:";
            ItemText.Content = "";
            init();
        }
    }
}
