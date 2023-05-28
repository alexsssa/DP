using System;
using System.Collections.Generic;
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
using System.Data.SQLite;
using System.IO;

namespace DP
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
        }

        public bool uninque_user()
        {
            string query = $"select id from users where login = '{inputlogin.Text}' and password = '{inputpassword.Password}'";

            SQLiteConnection sqlite = new SQLiteConnection("Data Source=database.db; Version=3;");

            using (sqlite)
            {
                SQLiteCommand sqlcmd;
                sqlite.Open();
                sqlcmd = sqlite.CreateCommand();
                sqlcmd.CommandText = query;
                SQLiteDataReader SQL = sqlcmd.ExecuteReader();

                if (SQL.HasRows)
                {
                    return true;
                }
                sqlite.Close();
                return false;
            }
        }

        private void button_ok(object sender, RoutedEventArgs e)
        {
            bool[] checks = new bool[] { false, false, false, false};
            string[] errors = new string[] { "Ошибка: Проверьте поле ввода логина", "Ошибка: Проверьте поле ввода пароля", "Ошибка: Пароли не совпадают", "Ошибка: Аккаунт уже создан" };


            if (inputlogin.Text.Length > 0) { checks[0] = true; }                      // логин введен

            if (inputpassword.Password.Length > 0) { checks[1] = true; }                 // пароль введен

            if (inputpassword.Password == inputpassword2.Password) { checks[2] = true; } // пароли совпадают

            if (!uninque_user()) { checks[3] = true; }


            if (checks.Contains(false)) { new Error(errors[Array.IndexOf(checks, false)]).Show(); }

            else
            {
                string query = $"insert into  users(login, password) values('{inputlogin.Text}', '{inputpassword.Password}')";

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
                new SuccessWindow("Новый аккаунт успешно создан!").Show();
                
                this.Close();
            }
        }
        private void button_exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
