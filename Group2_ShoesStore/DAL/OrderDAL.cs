using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Persistence;
namespace DAL
{
    public class OrderDAL
    {
        private MySqlDataReader reader;
        private string query;
        public OrderDAL() { }
        public bool CreateShoppingCart(Order order)
        {
            bool result = false;
            if (order == null)
            {
                return result;
            }
            MySqlConnection connection = DBHelper.OpenConnection();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = @"lock tables Orders write, Items write, OrderDetails write, Itemdetails write";
            command.ExecuteNonQuery();
            MySqlTransaction transaction = connection.BeginTransaction();
            command.Transaction = transaction;
            try
            {
                command.CommandText = "insert into Orders(order_customer,order_status) values (@UserID,@OrderStatus)";
                command.Parameters.AddWithValue("@UserID", order.OrderUser.UserID);
                command.Parameters.AddWithValue("@OrderStatus", order.OrderStatus);
                command.ExecuteNonQuery();
                string queryLastInsertId = $@"select order_id from Orders where order_customer = "+order.OrderUser.UserID+" order by order_id desc limit 1;";
                MySqlCommand selectLastId = new MySqlCommand(queryLastInsertId, connection);
                using (reader = selectLastId.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        order.OrderId = reader.GetInt32("order_id");

                    }
                }
                command.Parameters.Clear();
                command.CommandText = "insert into OrderDetails(order_id,item_id) values (@orderId,@itemId)";
                command.Parameters.AddWithValue("@orderId", order.OrderId);
                command.Parameters.AddWithValue("@itemId", order.OrderItem.ShoesId);
                // command.Parameters.AddWithValue("@Amount",order.Amount);
                command.ExecuteNonQuery();

                transaction.Commit();
                result = true;
            }
            catch (System.Exception e)
            {

                transaction.Rollback();
                Console.WriteLine(e);
                return result;
            }
            finally
            {
                command.CommandText = "unlock tables";
                command.ExecuteNonQuery();
                connection.Close();

            }

            return result;
        }

        public bool AddToShoppingcart(Order order)
        {

            bool result = false;

            MySqlConnection connection = DBHelper.OpenConnection();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = @"lock tables Orders write, Items write, OrderDetails write, Itemdetails write";
            command.ExecuteNonQuery();
            MySqlTransaction transaction = connection.BeginTransaction();
            command.Transaction = transaction;

            string queryLastInsertId = @"select order_id from Orders where order_customer = "+order.OrderUser.UserID+" order by order_id desc limit 1;";
            MySqlCommand selectLastId = new MySqlCommand(queryLastInsertId, connection);
            using (reader = selectLastId.ExecuteReader())
            {
                if (reader.Read())
                {
                    order.OrderId = reader.GetInt32("order_id");
                }
            }
            try
            {

                command.CommandText = "insert into OrderDetails(order_id,item_id) values (@orderId,@itemId)";
                command.Parameters.AddWithValue("@orderId", order.OrderId);
                command.Parameters.AddWithValue("@itemId", order.OrderItem.ShoesId);
                // command.Parameters.AddWithValue("@Amount", order.Amount);
                command.ExecuteNonQuery();
                transaction.Commit();
                result = true;

            }
            catch (System.Exception)
            {
                transaction.Rollback();
                return result;
            }
            finally
            {
                command.CommandText = "unlock tables";
                command.ExecuteNonQuery();
                connection.Close();
            }
            return result;
        }

        public bool DeleteItemInShoppingCartByIdItem(int? ShoesId)
        {
            if (ShoesId == null)
            {
                return false;
            }
            query = @"DELETE FROM Orderdetails where item_id = "+ShoesId+";";

            MySqlConnection connection = DBHelper.OpenConnection();
            if (DBHelper.ExecNonQuery(query, connection) == 0)
            {
                DBHelper.CloseConnection();
                return false;
            }
            DBHelper.CloseConnection();
            return true;
        }
        public bool DeleteAllItemInShoppingCartByUserID(int? UserID)
        {
            bool result = false;
            int orderId = -1;
            MySqlConnection connection = DBHelper.OpenConnection();

            MySqlCommand command = connection.CreateCommand();
            command.CommandText = @"lock tables Customers write, Orders write, Items write, Orderdetails write, Itemdetails write";
            command.ExecuteNonQuery();
            MySqlTransaction transaction = connection.BeginTransaction();
            command.Transaction = transaction;

            try
            {
                string queryLastInsertId = @"select max(order_id) as order_id from orders where order_customer = "+UserID+" order by order_id desc limit 1;";
                MySqlCommand selectLastId = new MySqlCommand(queryLastInsertId, connection);
                using (reader = selectLastId.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        orderId = reader.GetInt32("order_id");

                    }
                }

                command.Parameters.Clear();
                command.CommandText = @"DELETE FROM Orderdetails where order_id = "+orderId+";";
                command.ExecuteNonQuery();

                command.Parameters.Clear();
                command.CommandText = @"DELETE FROM orders where order_id = "+orderId+";";
                command.ExecuteNonQuery();

                transaction.Commit();
                result = true;
            }
            catch (System.Exception e)
            {
                transaction.Rollback();
                Console.WriteLine(e);
                return result;
            }
            finally
            {
                command.CommandText = "unlock tables";
                command.ExecuteNonQuery();
                connection.Clone();
            }
            return result;
        }


        public List<Shoes> ShowShopingCartByUserId(int? userId)
        {
            if (userId == null)
            {
                return null;
            }

            List<Shoes> listShoes = new List<Shoes>();
            query = @"select it.item_id, it.item_name, it.item_price from 
            Orders ord inner join Orderdetails ordt on ord.order_id = ordt.order_id 
            inner join Items it on ordt.item_id = it.item_id where ord.order_customer = "+userId+" and ord.order_status = 0 ;";
            try
            {
                reader = DBHelper.ExecQuery(query, DBHelper.OpenConnection());
            }
            catch (System.Exception)
            {
                Console.WriteLine("can not connect  ok");
                return null;
            }
            while (reader.Read())
            {
                listShoes.Add(GetItemShoppingCart(reader));
            }
            DBHelper.CloseConnection();
            return listShoes;

        }
        public bool CreateOrder(Order order)
        {

            bool result = false;
            if (order == null)
            {
                return result;
            }
            MySqlConnection connection = DBHelper.OpenConnection();

            MySqlCommand command = connection.CreateCommand();
            command.CommandText = @"lock tables Customers write, Orders write, Items write, Orderdetails write, Itemdetails write";
            command.ExecuteNonQuery();
            MySqlTransaction transaction = connection.BeginTransaction();
            command.Transaction = transaction;

            try
            {
                // command.CommandText = "insert into Orders(order_customer) values (@Customer_id)";
                // command.Parameters.AddWithValue("@Customer_id", order.OrderUser.UserID);
                // command.ExecuteNonQuery();
                string queryLastInsertId = $@"select order_id from Orders where order_customer = {order.OrderUser.UserID} order by order_id desc limit 1;";
                MySqlCommand selectLastId = new MySqlCommand(queryLastInsertId, connection);
                using (reader = selectLastId.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        order.OrderId = reader.GetInt32("order_id");

                    }
                }

                command.Parameters.Clear();
                // command.CommandText = "insert into OrderDetails(order_id,item_id) values (@Order_id,@item_id)";
                // command.Parameters.Clear();
                // command.Parameters.AddWithValue("@Order_id",order.OrderId);
                // command.Parameters.AddWithValue("@item_id",order.OrderItem.ShoesId);
                // command.Parameters.AddWithValue("@Amount",order.Amount);
                // command.ExecuteNonQuery();
                // command.CommandText = "update Itemdetails set item_quantity = item_quantity -@Amount where item_id = '"+order.OrderItem.ShoesId+"';";
                // command.Parameters.Clear();
                // command.Parameters.AddWithValue("@Amount",order.Amount);
                // command.ExecuteNonQuery();
                command.CommandText = $@"UPDATE Orders SET order_status = 1, order_date = NOW() where order_customer = {order.OrderUser.UserID} and order_id = {order.OrderId};";
                command.ExecuteNonQuery();
                transaction.Commit();
                result = true;
            }
            catch (System.Exception e)
            {
                transaction.Rollback();
                Console.WriteLine(e);
                return result;
            }
            finally
            {
                command.CommandText = "unlock tables";
                command.ExecuteNonQuery();
                connection.Clone();

            }
            return result;
        }
        public List<Order> ShowAllItemOrdered(int? userId)
        {
            if (userId == null)
            {
                return null;
            }

            List<Order> listOrders = new List<Order>();
            query = $@"select it.item_id, it.item_name, ord.order_date from 
            Orders ord inner join Orderdetails ordt on ord.order_id = ordt.order_id 
            inner join Items it on ordt.item_id = it.item_id
            where ord.order_customer = {userId} and ord.order_status = 1 group by it.item_name";
            try
            {
                reader = DBHelper.ExecQuery(query, DBHelper.OpenConnection());
            }
            catch (System.Exception)
            {
                Console.WriteLine("deo ket noi dc lan 1");
                return null;
            }
            while (reader.Read())
            {
                listOrders.Add(GetOrder(reader));
            }
            DBHelper.CloseConnection();
            return listOrders;
        }
        public Order GetLastOrderIdPurchase(int? userId)
        {
            if (userId == null)
            {
                return null;
            }

            Order order = null;
            query = @"select max(order_id) from Orders where order_customer = "+userId+" ";
            reader = DBHelper.ExecQuery(query, DBHelper.OpenConnection());
            if (reader.Read())
            {
                order = GetOrder(reader);
            }
            reader.Close();
            DBHelper.CloseConnection();
            return order;
        }
        public List<Order> ShowOrderUserPaySucess(int? userId)
        {
            if (userId == null)
            {
                return null;
            }

            List<Order> orders = new List<Order>();
            query = @"select ord.order_id as order_id, ord.order_date, it.item_id ,it.item_name, it.item_price, cus.customer_name, cus.customer_email from 
            Customers cus inner join orders ord on ord.order_customer = cus.customer_id inner join Orderdetails ordt on ord.order_id = ordt.order_id 
            inner join Items it on ordt.item_id = it.item_id
             where ord.order_customer = "+userId+" and ord.order_id = "+GetLastInsertOrderID(userId)+";";
            reader = DBHelper.ExecQuery(query, DBHelper.OpenConnection());
            while (reader.Read())
            {
                orders.Add(GetOrderPurchaseSuccess(reader));
            }
            reader.Close();
            DBHelper.CloseConnection();
            return orders;
        }
        
        
        private Order GetOrderPurchaseSuccess(MySqlDataReader reader)
        {
            Order order = new Order();
            order.OrderItem = new Shoes();
            order.OrderUser = new Customer();
            order.OrderId = reader.GetInt32("order_id");
            order.OrderUser.UserName = reader.GetString("customer_name");
            order.OrderUser.UserEmail = reader.GetString("customer_email");
            order.OrderItem.ShoesId = reader.GetInt32("item_id");
            order.OrderItem.ShoesPrice = reader.GetDouble("item_price");
            order.OrderDate = reader.GetDateTime("order_date");
            order.OrderItem.ShoesName = reader.GetString("item_name");
            // order.Amount = reader.GetInt32("amount");
            return order;
        }
        public int GetLastInsertOrderID(int? UserID)
        {
            int orderId = -1;

            string queryLastInsertId = @"select order_id from Orders where order_customer = "+UserID+" order by order_id desc limit 1;";
            reader = DBHelper.ExecQuery(queryLastInsertId, DBHelper.OpenConnection());
            if (reader.Read())
            {
                orderId = reader.GetInt32("order_id");

            }
            reader.Close();
            return orderId;
        }
        private Order GetOrder(MySqlDataReader reader)
        {
            Order order = new Order();
            order.OrderItem = new Shoes();
            order.OrderItem.ShoesId = reader.GetInt32("item_id");
            order.OrderItem.ShoesName = reader.GetString("item_name");
            // order.Amount = reader.GetInt32("amount");
            order.OrderDate = reader.GetDateTime("order_date");
            return order;
        }
        private Shoes GetItemShoppingCart(MySqlDataReader reader)
        {
            Shoes item = new Shoes();
            item.ShoesId = reader.GetInt32("item_id");
            item.ShoesName = reader.GetString("item_name");
            item.ShoesPrice = reader.GetDouble("item_price");
            return item;
        }
    }

}