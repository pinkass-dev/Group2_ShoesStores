using System;
using System.Text.RegularExpressions;
using DAL;
using Persistence;
namespace BL
{
    public class CustomerBL
    {
        private CustomerDAL customerDAL;
        public CustomerBL()
        {
            customerDAL = new CustomerDAL();
        }
        public Customer GetCustomerByUserNameAndPassWord(string username, string password)
        {
            if (username == null || password == null)
            {
                return null;
            }
            Regex regex = new Regex("[a-zA-Z0-9_]");
            MatchCollection matchCollectionUsername = regex.Matches(username);
            MatchCollection matchCollectionPassword = regex.Matches(password);
            if (matchCollectionUsername.Count < username.Length || matchCollectionPassword.Count < password.Length)
            {
                return null;
            }
            return customerDAL.GetCustomerByUserNameAndPassWord(username,password);
        }
        public Customer GetCustomerByID(int? UserID)
        {
            return customerDAL.GetCustomerByID(UserID);
        }
         public bool UpdateStatusShoppingCartById(bool isHave, int? UserID)
        {
            return customerDAL.UpdateStatusShoppingCartById(isHave, UserID);
        }
        public int VerifyRegister(string Username, string Email)
        {
            return customerDAL.VerifyRegister(Username,Email);
        }
        public int Register(string Username, string Password, string Name, string Email, string Phone, string Birthday, string Gender, string Address)
        {
           return customerDAL.Register(Username, Password,  Name, Email, Phone,  Birthday, Gender, Address);
        }

    }
    
}