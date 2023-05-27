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

namespace WpfApp3
{
    /// <summary>
    /// Логика взаимодействия для Autorization.xaml
    /// </summary>
    public partial class Autorization : Window
    {
        // подключение созданого класса - DataBase.cs
        DataBase dataBase = new DataBase();
        public Autorization()
        {
            InitializeComponent();
        }
        private void button_done(object sender, RoutedEventArgs e)
        {
            // Максимальная длина строк как и базе данных
            inputlogin.MaxLength = 50;
            inputpassword.MaxLength = 50;

            // Переменные для работы с базой данных
            var loginUser = inputlogin.Text;
            var passwordUser = inputpassword.Password;

            // создание адаптера базы данных
            SqlDataAdapter adapter = new SqlDataAdapter();

            // создание объекта класса таблицы для сверки с БД
            DataTable table = new DataTable();

            // создание запроса, для проверки Логина и Пароля
            string querystring = $"select id_user, login_user, password_user from register where login_user = '{loginUser}' and password_user = '{passwordUser}'";

            // связь с бд
            SqlCommand command = new SqlCommand(querystring, dataBase.getConnection());

            // для получения данных с БД
            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (inputlogin.Text.Length > 0)
            {
                if (inputpassword.Password.Length > 0)
                {
                    if (table.Rows.Count == 1)
                    {
                        new SuccessWindow("Вход выполнен!").Show();
                        new Main().Activate();
                        this.Close();
                    }
                }
                else { new Error("Ошибка: Проверьте поле ввода пароля").Show(); }
            }
            else { new Error("Ошибка: Проверьте поле ввода логина").Show(); }
            dataBase.CloseConnection();
        }

        private void button_exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void button_register(object sender, RoutedEventArgs e)
        {
            new Registration().ShowDialog();
        }
    }
}
