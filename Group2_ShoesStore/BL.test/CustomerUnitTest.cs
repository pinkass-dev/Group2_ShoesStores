using System;
using Xunit;
using Persistence;
using BL;

namespace BL.test
{
public class CustomerUnitTest
    {
        private CustomerBL customerBL = new CustomerBL();


        [Theory]
        [InlineData("xuan", "123456")]
        public void GetCustomerByUserNameAndPassWordTest(string username, string password)
        {
            Customer customer = customerBL.GetCustomerByUserNameAndPassWord(username, password);
            Assert.NotNull(customer);
            Assert.Equal(username, customer.UserAccount);
            Assert.Equal(password, customer.UserPassWord);
        }
        [Theory]
        [InlineData("'?/:%'", "'.:=='")]
        [InlineData("xuantieu", "123456789")]
        [InlineData("null", "'.:=='")]
        [InlineData("'.:=='", "null")]
        public void GetUserByUserNameAndPassWordTest1(string username, string password)
        {
            Assert.Null(customerBL.GetCustomerByUserNameAndPassWord(username, password));
        }
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void GetCustomerByIdTest(int? userId)
        {
            Assert.NotNull(customerBL.GetCustomerByID(userId));
        }
        [Theory]
        [InlineData(0)]
        [InlineData(null)]

        public void GetCustomerByIdTest1(int? userId)
        {
            Assert.Null(customerBL.GetCustomerByID(userId));
        }

    }
}