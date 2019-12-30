using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;
using Persistence;
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
                DBHelper.OpenConnection();
                reader = DBHelper.ExecQuery(query );
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
        public Customer GetCustomerByID(int UserID)
        {
            if (UserID == null)
            {
                return null;
            }
            query = $@"select * from Customers where customer_id = {UserID};";
            DBHelper.OpenConnection();
            reader = DBHelper.ExecQuery(query);
            Customer cus = null;
            if (reader.Read())
            {
                cus = GetCustomer(reader);
            }
            reader.Close();
            DBHelper.CloseConnection();
            return cus;
        }

        private Customer GetCustomer(MySqlDataReader reader)
        {
            Customer cus = new Customer();
            cus.UserID = reader.GetInt32("customer_id");
            cus.UserAccount = reader.GetString("customer_account");
            cus.UserPassWord = reader.GetString("customer_password");
            cus.UserName = reader.GetString("customer_name");
            cus.CusName = reader.GetString("customer_name");
            cus.UserMail = reader.GetString("customer_email");
            cus.UserAdd = reader.GetString("customer_address");
            cus.UserPhone = reader.GetString("customer_phone");
            cus.UserGender = reader.GetString("customer_gender");
            cus.UserBirthDay = reader.GetString("customer_birthday");
            return cus;
        }
    }

}