using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Persistence;

namespace DAL
{
    public class ShoesDAL
    {
        private string query;
        private MySqlDataReader reader;
        public Shoes GetShoesByName(string ShoesName)
        {
            query = @"select item_name, item_size, item_price, item_color,
             item_quantity,item_material, item_trademark
            from Items where item_name ='" + ShoesName + "';";
            DBHelper.OpenConnection();
            reader = DBHelper.ExecQuery(query);
            Shoes shoes = null;
            if (reader.Read())
            {
                shoes = GetShoesInfo(reader);
            }
            DBHelper.CloseConnection();
            return shoes;
        }
        private Shoes GetShoesInfo(MySqlDataReader reader)
        {
            Shoes sh = new Shoes();
            sh.ShoesName = reader.GetString("item_name");
            sh.ShoesPrice = reader.GetInt32("item_price");
            sh.ShoesQuantity = reader.GetInt32("item_quantity");
            sh.ShoesSize = reader.GetInt32("item_size");
            sh.ShoesColor = reader.GetString("item_color");
            sh.ShoesMaterial = reader.GetString("item_material");
            sh.ShoesBrand = reader.GetString("item_trademark");
            return sh;
        }

        public List<Shoes> GetListShoes()
        {
            DBHelper.OpenConnection();
            query = @"select * from Items limit 10;";
            List<Shoes> shoes = new List<Shoes>();
            try
            {
                reader = DBHelper.ExecQuery(query);
            }
            catch (System.Exception)
            {
                Console.WriteLine("Can not connect database!");
                return null;
            }
            if (reader.Read())
            {
                // shoes = GetShoesInfo(reader);
                shoes.Add(GetShoesInfo(reader));
            }
            DBHelper.CloseConnection();
            return shoes;
        }
    }
}