using System;
using Xunit;
using DAL;
using Persistence;
namespace DAL.Test
{
    public class CustomerUnitTest
    {
        private CustomerDAL customerDAL = new CustomerDAL();
        [Theory]
        [InlineData("xuan", "123456")]
        public void GetCustomerByUserNameAndPassWordTest(string username, string password)
        {
            Customer customer = customerDAL.GetCustomerByUserNameAndPassWord(username, password);
            Assert.NotNull(customer);
            Assert.Equal(username, customer.UserAccount);
            Assert.Equal(password, customer.UserPassWord);
        }
        [Theory]
        [InlineData("'?/:%'", "'.:=='")]
        [InlineData("xuantieu", "123456789")]
        [InlineData("saadasd", "saadasd")]
        [InlineData("adassad", "dasq")]

        public void GetCustomerByUserNameAndPassWordTest1(string username, string password)
        {
            Assert.Null(customerDAL.GetCustomerByUserNameAndPassWord(username, password));
        }
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void GetCustomerByIdTest(int? UserID)
        {
            Assert.NotNull(customerDAL.GetCustomerByID(UserID));
        }
        [Theory]
        [InlineData(0)]
        [InlineData(null)]

        public void GetCustomerByIdTest1(int? userId)
        {
            Assert.Null(customerDAL.GetCustomerByID(userId));
        }
        // [Theory]
        // [InlineData(null)]
        // public void RegisterTest(string Username, string Password, string Name, string Email, string Phone, string Birthday, string Gender, string Address)
        // {
        //     Assert.NotNull(customerDAL.Register(Username,Password,Name,Email,Phone,Birthday,Gender,Address));

        // }
        // public void RegisterTest1(string Username, string Password, string Name, string Email, string Phone, string Birthday, string Gender, string Address)
        // {

        // }
    }

}
