using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
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
        // public Shoes GetShoesByName(string ShoesName)
        // {
        //     return shoesDAL.GetShoesByName(ShoesName);
        // }
        public List<Shoes> GetListShoes()
        {
            return shoesDAL.GetListShoes();
        }
        public List<Shoes> SearchShoesName()
        {
            return shoesDAL.SearchShoesName();
        }
        public List<Shoes> SearchShoesName(string shoesName)
        {
            shoesName = shoesName.ToLower();
            Console.WriteLine(shoesName);
            List<Shoes> shoes = new List<Shoes>();
            List<Shoes> newshoes = new List<Shoes>();
            shoes = shoesDAL.SearchShoesName();
            foreach (var item in shoes)
            {
                if (item.ShoesName.ToLower().Contains(shoesName))
                {
                    newshoes.Add(item);
                }
            }
            return newshoes;
        }
        public Shoes GetShoesById(int? ShoesId)
        {
            if (ShoesId == null)
            {
                return null;
            }
            Regex regex = new Regex("[0-9]");
            MatchCollection matchCollectionId = regex.Matches(ShoesId.ToString());
            if (matchCollectionId.Count< ShoesId.ToString().Length)
            {
                return null;
            }
            return shoesDAL.GetShoesById(ShoesId);
        }
    }

}
