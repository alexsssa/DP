using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace WpfApp3
{
    internal class DataBase
    {
        // Создания подключения с БД
        SqlConnection sqlConnection = new SqlConnection(@"Data Source=WIN-JO22SGESGIG; Initial Catalog=users_db; Integrated Security=True;");
        
        // Открытие подключение с БД
        public void OpenConnection()
        {
            // проверка на открытие подключения
            if(sqlConnection.State == System.Data.ConnectionState.Closed) sqlConnection.Open();
        }

        // Закрытие подключения с БД
        public void CloseConnection()
        {
            // Проверка на закрытие подключения
            if(sqlConnection.State == System.Data.ConnectionState.Open) sqlConnection.Close();
        }

        // Получения подключения БД
        public SqlConnection getConnection()
        {
            // Возращает подключение
            return sqlConnection;
        }
    }
}
