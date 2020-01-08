using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;
using Persistence;
using System.Security.Cryptography;
namespace DAL
{
    public class CustomerDAL
    {
        private MySqlDataReader reader;
        private string query;
        public CustomerDAL() { }
        
        public Customer GetCustomerByUserNameAndPassWord(string username, string password)
        {
            if ((username == null) || (password == null))
            {
                return null;
            }
            Regex regex = new Regex("[a-zA-Z0-9_]");
            MatchCollection matchCollectionUserName = regex.Matches(username);
            MatchCollection matchCollectionPassWord = regex.Matches(password);
            if (matchCollectionUserName.Count < username.Length || matchCollectionPassWord.Count < password.Length)
            {
                return null;
            }

            query = $@"select * from Customers where customer_account = '{username}' and customer_password = '{password}'";
            try
            {

                reader = DBHelper.ExecQuery(query, DBHelper.OpenConnection());
            }
            catch (System.Exception)
            {

                Console.WriteLine("Can not connect to database!");
                return null;
            }
            Customer customer = null;
            if (reader.Read())
            {
                customer = GetCustomer(reader);
            }
            reader.Close();
            DBHelper.CloseConnection();
            return customer;


        }
        public Customer GetCustomerByID(int? UserID)
        {
            if (UserID == null)
            {
                return null;
            }
            query = $@"select * from Customers where customer_id = " + UserID + ";";

            reader = DBHelper.ExecQuery(query, DBHelper.OpenConnection());
            Customer cus = null;
            if (reader.Read())
            {
                cus = GetCustomer(reader);
            }
            reader.Close();
            DBHelper.CloseConnection();
            return cus;
        }
        public bool UpdateStatusShoppingCartById(bool isHave, int? UserID)
        {

            if (UserID == null)
            {
                return false;
            }

            switch (isHave)
            {
                case true:
                    
                    query = @"update Customers set UserShoppingCart = false where customer_id = " + UserID + ";";
                    
                    break;
                case false:
                    query = @"update Customers set UserShoppingCart = true where customer_id = " + UserID + ";";
                    break;
            }

            DBHelper.ExecNonQuery(query, DBHelper.OpenConnection());
            DBHelper.CloseConnection();
            return true;
        }

        private Customer GetCustomer(MySqlDataReader reader)
        {
            Customer cus = new Customer();
            cus.UserID = reader.GetInt32("customer_id");
            cus.UserAccount = reader.GetString("customer_account");
            cus.UserPassWord = reader.GetString("customer_password");
            cus.UserName = reader.GetString("customer_name");
            cus.UserEmail = reader.GetString("customer_email");
            cus.UserAdd = reader.GetString("customer_address");
            cus.UserPhone = reader.GetString("customer_phone");
            cus.UserGender = reader.GetString("customer_gender");
            cus.UserBirthDay = reader.GetString("customer_birthday");
            return cus;
        }
        public int VerifyRegister(string Username, string Email)
        {
            int a;
            MySqlConnection connection = DBHelper.OpenConnection();
            query = @"Select * from Customers where customer_account = '" + Username + "' or customer_email ='" + Email + "';";
            MySqlCommand command = new MySqlCommand(query,connection);
            reader = command.ExecuteReader();
            if (reader.Read())
            {
                System.Console.WriteLine("Account or email already exists");
                a = 1;
            }
            else
            {
                System.Console.WriteLine("Registration successful");
                a = 2;
            }
            reader.Close();
            DBHelper.CloseConnection();
            return a;
        }
        public int Register(string Username, string Password, string Name, string Email, string Phone, string Birthday, string Gender, string Address)
        {
            
            try
            {
                MySqlConnection connection = DBHelper.OpenConnection();
                
                query = @"insert into Customers(customer_account,customer_password,customer_name, customer_email,customer_phone,customer_birthday,customer_gender,customer_address) values ('"+Username+"','"+Password+"','"+Name+"','"+Email+"','"+Phone+"','"+Birthday+"','"+Gender+"','"+Address+"');";
                
                MySqlCommand command = new MySqlCommand(query,connection);
                command.ExecuteNonQuery();
            }
            catch
            {
                Console.WriteLine("could not be registered!");
            }
            finally
            {
                DBHelper.CloseConnection();
            }
            return 1;
        }
    }

}