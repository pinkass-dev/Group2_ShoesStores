using Xunit;
using System;
using DAL;
namespace DAL.Test
{
    public class ShoesUnitTest
    {

        ShoesDAL shoesDAL = new ShoesDAL();
        [Fact]
        public void GetListShoesTest()
        {
            Assert.NotNull(shoesDAL.GetListShoes());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void GetShoesByIdTest(int? itemId)
        {
            Assert.NotNull(shoesDAL.GetShoesById(itemId));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(null)]
        public void GetShoesByIdTest1(int? itemId)
        {
            Assert.Null(shoesDAL.GetShoesById(itemId));
        }

    }
}