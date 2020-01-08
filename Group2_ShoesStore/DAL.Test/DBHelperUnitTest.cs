using System;
using Xunit;
using DAL;

namespace DAL.Test
{
    public class DBHelperUnitTest1
    {
        [Fact]
        public void OpenConnectionTest()
        {
            Assert.NotNull(DBHelper.OpenConnection());

        }
    }
}
