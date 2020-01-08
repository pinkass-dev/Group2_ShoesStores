using System;
using Xunit;
using Persistence;
using BL;
namespace DAL.Test
{
    public class ShoesUnitTest
    {
        private ShoesBL shoesBL = new ShoesBL();
        [Fact]
        public void GetListItemsTest()
        {
            Assert.NotNull(shoesBL.GetListShoes());
        }
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void GetShoesByIdTest(int? itemId)
        {
            Assert.NotNull(shoesBL.GetShoesById(itemId));
        }
        [Theory]
        [InlineData(0)]
        [InlineData(null)]
        public void GetShoesByIdTest1(int? itemId)
        {
            Assert.Null(shoesBL.GetShoesById(itemId));
        }

    }
}
