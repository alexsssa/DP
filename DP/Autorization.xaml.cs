using System;
using System.Windows;
using System.Windows.Controls;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;

namespace DP
{
    /// <summary>
    /// Логика взаимодействия для Autorization.xaml
    /// </summary>
    public partial class Autorization : Window
    {

        public Autorization()
        {
            InitializeComponent();
        }

        public bool search_user(ref int id_user)
        {
            string query = $"select ID from users where login = '{inputlogin.Text}' and password = '{inputpassword.Password}'";

            SQLiteConnection sqlite = new SQLiteConnection("Data Source=database.db; Version=3;");

            using (sqlite)
            {
                SQLiteCommand sqlcmd;
                sqlite.Open();
                sqlcmd = sqlite.CreateCommand();
                sqlcmd.CommandText = query;
                SQLiteDataReader SQL = sqlcmd.ExecuteReader();

                var values = new List<string>();
                if (SQL.Read())
                {
                    id_user = Convert.ToInt32(SQL["ID"]);
                }

                if (SQL.HasRows)
                {
                    
                    return true;
                }
                sqlite.Close();
                return false;
            }
        }

        private void button_done(object sender, RoutedEventArgs e)
        {
            int id = -1;
            if (inputlogin.Text.Length > 0)
            {
                if (inputpassword.Password.Length > 0)
                {
                    if (search_user(ref id))
                    {
                        SuccessWindow window = new SuccessWindow("Вход выполнен!");
                        window.Show();
                        Thread.Sleep(3000);
                        window.Close();
                        new Main(id, inputlogin.Text).Show();
                        this.Close();
                    }
                    else { new Error("Ошибка: Пользователь не найден").Show(); }
                }
                else { new Error("Ошибка: Проверьте поле ввода пароля").Show(); }
            }
            else { new Error("Ошибка: Проверьте поле ввода логина").Show(); }
        }


        private void button_exit(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
            
        }

        private void button_register(object sender, RoutedEventArgs e)
        {
            new Registration().Show();
        }
    }
}
