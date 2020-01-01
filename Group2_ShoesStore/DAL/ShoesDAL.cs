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
        // public Shoes GetShoesByName(string ShoesName)
        // {
        //     query = @"select item_name, item_size, item_price, item_color,
        //      item_quantity,item_material, item_trademark
        //     from Items where item_name ='" + ShoesName + "';";
        //     DBHelper.OpenConnection();
        //     reader = DBHelper.ExecQuery(query);
        //     Shoes shoes = null;
        //     if (reader.Read())
        //     {
        //         shoes = GetShoesInfo(reader);
        //     }
        //     DBHelper.CloseConnection();
        //     return shoes;
        // }
public List<Shoes> SearchShoesName()
{
    query = @"select * from Items;";
    List<Shoes> shoes = new List<Shoes>();
    
    try
    {
        
        reader = DBHelper.ExecQuery(query,DBHelper.OpenConnection());
    }
    catch (System.Exception)
    {
        
    Console.WriteLine("can not connect to database!");
    return null;
    }
    while (reader.Read())
    {
        shoes.Add(GetShoesInfo(reader));
    }
    reader.Close();
    DBHelper.CloseConnection();
    return shoes;
}
        
        public List<Shoes> GetListShoes()
        {
            
            query = @"select it.item_id,it.item_name,it.item_price,it.item_color,it.item_material,it.item_trademark,itd.item_size,itd.item_quantity from items it, itemdetails itd where it.item_id = itd.item_id;";
            List<Shoes> shoes = new List<Shoes>();
            try
            {
                reader = DBHelper.ExecQuery(query, DBHelper.OpenConnection());
            }
            catch (System.Exception)
            {
                Console.WriteLine("Can not connect database!");
                return null;
            }
            if (reader.Read())
            {
                shoes.Add(GetShoesInfo(reader));
            }
            reader.Close();
            DBHelper.CloseConnection();
            return shoes;
        }
        public Shoes GetShoesById(int? ShoesId)
        {
            if (ShoesId == null)
            {
                return null;
            }
            
            query = $"select * from Items where item_id = {ShoesId}";
            reader = DBHelper.ExecQuery(query,DBHelper.OpenConnection());
            Shoes shoes = null;
            if (reader.Read())
            {
                shoes = GetShoesInfo(reader);
            }
            reader.Close();
            DBHelper.CloseConnection();
            return shoes;
        }
        public List<Shoes> SearchItem(int temp)
        {
            
            switch (temp)
            {
                case 1:
                query = $"select * from Items where item_id = ";
                break;
            }
            reader = DBHelper.ExecQuery(query,DBHelper.OpenConnection());
            List<Shoes> shoes = new List<Shoes>();
            while (reader.Read())
            {
                shoes.Add(GetShoesInfo(reader));
            }
            reader.Close();
            DBHelper.CloseConnection();
            return shoes;
        }
        private Shoes GetShoesInfo(MySqlDataReader reader)
        {
            Shoes shoes = new Shoes();
            shoes.ShoesName = reader.GetString("item_name");
            shoes.ShoesPrice = reader.GetInt32("item_price");
            shoes.ShoesQuantity = reader.GetInt32("item_quantity");
            shoes.ShoesSize = reader.GetInt32("item_size");
            shoes.ShoesColor = reader.GetString("item_color");
            shoes.ShoesMaterial = reader.GetString("item_material");
            shoes.ShoesBrand = reader.GetString("item_trademark");
            return shoes;
        }

    }
    
}