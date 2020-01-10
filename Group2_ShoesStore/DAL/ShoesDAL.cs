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
        
public List<Shoes> SearchShoesName()
{
    query = @"select it.item_id,it.item_name,itd.item_size,it.item_price,it.item_color,it.item_material,it.item_trademark,itd.item_quantity from items it, itemdetails itd where it.item_id = itd.item_id;";
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
            
            DBHelper.OpenConnection();
            // page = (page -1)*10;
            query = @"select it.item_id,it.item_name,it.item_price,it.item_color,it.item_material,it.item_trademark,itd.item_size,itd.item_quantity from items it, itemdetails itd where it.item_id = itd.item_id limit 10;";
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
            while (reader.Read())
            {
                shoes.Add(GetShoesInfo(reader));
            }
            reader.Close();
            DBHelper.CloseConnection();
            return shoes;
        }
        public Shoes GetShoesById(int? itemId)
        {
            if (itemId == null)
            {
                return null;
            }
            
            query = $@"select it.item_id,it.item_name,itd.item_size,it.item_price,it.item_color,it.item_material,it.item_trademark,itd.item_quantity from Items it, Itemdetails itd where it.item_id = "+ itemId+";";
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
                query = $@"select it.item_id,it.item_name,itd.item_size,it.item_price,it.item_color,it.item_material,it.item_brand,itd.item_quantity from Items it, Itemdetails itd where item_id = ";
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

        public List<Shoes> PagingItems(int pageNo, int itemPerPAge)
        {

            DBHelper.OpenConnection();

            query = $@"select * from items limit {pageNo},{itemPerPAge} order by id OFFSET 10 ROW FETCH next 10 rows only";
            List<Shoes> shoes = new List<Shoes>();
            reader = DBHelper.ExecQuery(query, DBHelper.OpenConnection());
            while (reader.Read())
            {
                shoes.Add(GetShoesInfo(reader));
            }
            reader.Close();
            DBHelper.CloseConnection();

            return shoes;
        }
        public int GetTotalPage()
        {


            query = @"select count(*) / 10 from items;";
            var command = new MySqlCommand(query, DBHelper.OpenConnection());
            int count = Convert.ToInt32(command.ExecuteScalar());
            DBHelper.CloseConnection();
            return count;
        }
        private Shoes GetShoesInfo(MySqlDataReader reader)
        {
            Shoes shoes = new Shoes();
            shoes.ShoesId = reader.GetInt32("item_id");
            shoes.ShoesName = reader.GetString("item_name");
            shoes.ShoesSize = reader.GetString("item_size");
            shoes.ShoesPrice = reader.GetInt32("item_price");
            shoes.ShoesColor = reader.GetString("item_color");
            shoes.ShoesMaterial = reader.GetString("item_material");
            shoes.ShoesBrand = reader.GetString("item_trademark");
            shoes.ShoesQuantity = reader.GetInt32("item_quantity");
            return shoes;
        }


    }
    
}