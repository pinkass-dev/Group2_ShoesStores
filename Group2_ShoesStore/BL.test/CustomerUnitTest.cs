using System;
using Xunit;
using Persistence;
using BL;
namespace DAL.Test
{
    public class CustomerUnitTest
    {
        private CustomerBL customerBL = new CustomerBL();
        [theory]
        [inlinedata("xuan", "123456")]
        public void GetCustomerByUserNameAndPassWordTest(string username, string password)
        {
            Customer customer = customerBL.GetCustomerByUserNameAndPassWord(username, password);
            Assert.NotNull(user);
            Assert.Equal(username, user.UserAccount);
            Assert.Equal(password, user.UserPassword);
        }
        [Theory]
        [InlineData("'?/:%'", "'.:=='")]
        [InlineData("asas03", "1324567898456")]
        [InlineData("null", "'.:=='")]
        [InlineData("'.:=='", "null")]
        public void GetUserByUserNameAndPassWordTest1(string username, string password)
        {
            Assert.Null(customerBL.GetCustomerByUserNameAndPassWord(username, password));
        }
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void GetUserByIdTest(int? userId)
        {
            Assert.NotNull(customerBL.GetCustomerByID(userId));
        }
        [Theory]
        [InlineData(0)]
        [InlineData(null)]

        public void GetUserByIdTest1(int? userId)
        {
            Assert.Null(customerBL.GetCustomerByID(userId));
        }

    }
}