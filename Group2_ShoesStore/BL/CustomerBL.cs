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
        public Customer GetCustomerByID(int UserID)
        {
            return customerDAL.GetCustomerByID(UserID);
        }
         public bool UpdateStatusShoppingCartById(bool isHave, int? userId)
        {
            return customerDAL.UpdateStatusShoppingCartById(isHave, userId);
        }

    }
    
}