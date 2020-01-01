using System;
namespace Persistence
{
    public class Customer
    {
        public Customer() { }
        public int UserID { get; set; }
        public string UserAccount { get; set; }
        public string UserPassWord { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserAdd { get; set; }
        public string UserPhone { get; set; }
        public string UserGender { get; set; }
        public string UserBirthDay { get; set; }
        public bool UserShoppingCart{get;set;}
    }

}