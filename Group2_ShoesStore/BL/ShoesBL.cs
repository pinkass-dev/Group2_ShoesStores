using System;
using System.Collections.Generic;
using DAL;
using Persistence;

namespace BL
{
    public class ShoesBL
    {
        private ShoesDAL shoesDAL;
        public ShoesBL()
        {
            shoesDAL = new ShoesDAL();
        }
        public Shoes GetShoesByName(string ShoesName)
        {
            return shoesDAL.GetShoesByName(ShoesName);
        }
        public List<Shoes> GetListShoes()
        {
            return shoesDAL.GetListShoes();
        }
    }

}
