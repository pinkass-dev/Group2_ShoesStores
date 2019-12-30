using System;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class DBHelper
    {
        private static MySqlConnection connection;
        public static MySqlConnection GetConnection()
        {
            if (connection == null)
            {
                connection = new MySqlConnection
                {
                    ConnectionString = @"server = localhost; user id =root; port =3306;password=Lnx846061;database = shoesstore"
                };
            }
            return connection;
        }
        public static MySqlConnection OpenConnection()
        {
            if (connection ==null)
            {
                GetConnection();
            }
            connection.Open();
            return connection;
        }
        public static void CloseConnection()
        {
            if (connection !=null)
            {
                connection.Close();
            }
        }
        public static MySqlDataReader ExecQuery(string query)
        {
            MySqlCommand command = new MySqlCommand(query, connection);
            return command.ExecuteReader();
        }
    }

}
