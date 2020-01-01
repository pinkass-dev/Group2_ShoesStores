using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.IO;


namespace DAL
{
    public class DBHelper
    {
        private static MySqlConnection connection = null;
        public static MySqlConnection OpenConnection()
        {
            try
            {
                string connectionString;

                FileStream fileStream = File.OpenRead("ConnectionString.txt");
                using (StreamReader reader = new StreamReader(fileStream))
                {
                    connectionString = reader.ReadLine();
                }
                fileStream.Close();

                return OpenConnection(connectionString);
            }
            catch
            {
                return null;
            }
        }

        public static MySqlConnection OpenConnection(string connectionString)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection
                {
                    ConnectionString = connectionString
                };
                connection.Open();
                return connection;
            }
            catch
            {
                return null;
            }
        }
        public static void CloseConnection()
        {
            if (connection != null)
            {
                connection.Close();
            }
        }
        public static MySqlDataReader ExecQuery(string query, MySqlConnection connection)
        {
            MySqlCommand command = new MySqlCommand(query, connection);
           
            return command.ExecuteReader();
        }
        public static int ExecNonQuery(string query, MySqlConnection connection)
        {
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = query;

            return command.ExecuteNonQuery();
        }
        public static bool ExecTransaction(List<string> queries)
        {
            bool result = false;
            OpenConnection();
            MySqlCommand command = connection.CreateCommand();
            MySqlTransaction trans = connection.BeginTransaction();

            command.Connection = connection;
            command.Transaction = trans;
            try
            {
                foreach (var query in queries)
                {
                    command.CommandText = query;
                    command.ExecuteNonQuery();
                }
                trans.Commit();
                result = true;
            }
            catch (System.Exception e)
            {
                result = false;
                trans.Rollback();
                Console.WriteLine(e);
            }
            finally
            {
                CloseConnection();
            }

            return result;
        }
        // private static MySqlConnection connection;
        // public static MySqlConnection GetConnection()
        // {
        //     if (connection == null)
        //     {
        //         connection = new MySqlConnection
        //         {
        //             ConnectionString = @"server = localhost; user id =root; port =3306;password=Lnx846061;database = shoesstore"
        //         };
        //     }
        //     return connection;
        // }
        // public static MySqlConnection OpenConnection()
        // {
        //     if (connection == null)
        //     {
        //         GetConnection();
        //     }
        //     connection.Open();
        //     return connection;
        // }
        // public static void CloseConnection()
        // {
        //     if (connection != null)
        //     {
        //         connection.Close();
        //     }
        // }
        // public static MySqlDataReader ExecQuery(string query, MySqlConnection connection)
        // {
        //     MySqlCommand command = new MySqlCommand(query, connection);
        //     return command.ExecuteReader();
        // }


        // public static bool ExecTransaction(List<string> queries)
        // {
        //     bool result = false;
        //     OpenConnection();
        //     MySqlCommand command = connection.CreateCommand();
        //     MySqlTransaction trans = connection.BeginTransaction();

        //     command.Connection = connection;
        //     command.Transaction = trans;
        //     try
        //     {
        //         foreach (var query in queries)
        //         {
        //             command.CommandText = query;
        //             command.ExecuteNonQuery();
        //         }
        //         trans.Commit();
        //         result = true;
        //     }
        //     catch (System.Exception e)
        //     {
        //         result = false;
        //         trans.Rollback();
        //         Console.WriteLine(e);
        //     }
        //     finally
        //     {
        //         CloseConnection();
        //     }

        //     return result;
        // }

        // public static int ExecNonQuery(string query, MySqlConnection connection)
        // {
        //     MySqlCommand command = connection.CreateCommand();
        //     command.CommandText = query;

        //     return command.ExecuteNonQuery();
        // }
        
    }

}
