using System;
using Xunit;
using Persistence;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
namespace DAL.Test
{

    public class OrderUnitTest
    {
        private MySqlDataReader reader;
        OrderDAL orderDAL = new OrderDAL();
        [Fact]
        public void CreateShoppingCartTest(int amount)
        {
            CustomerDAL customerDAL = new CustomerDAL();
            Order order = new Order();
            ShoesDAL shoesDAL = new ShoesDAL();
            order.OrderUser = new Customer();
            order.OrderItem = new Shoes();
            order.OrderStatus = 0;
            order.OrderItem = shoesDAL.GetShoesById(2);
            order.OrderUser = customerDAL.GetCustomerByID(1);
            Assert.True(orderDAL.CreateShoppingCart(order, amount));
            orderDAL.DeleteAllItemInShoppingCartByUserID(1);
        }
        [Fact]
        public void CreateShoppingCartTest1(int amount)
        {
            CustomerDAL customerDAL = new CustomerDAL();
            Order order = new Order();
            order.OrderUser = new Customer();
            order.OrderItem = new Shoes();

            order.OrderStatus = 0;
            order.OrderUser.UserID = 0;
            order.OrderItem.ShoesId = 0;

            Assert.False(orderDAL.CreateShoppingCart(order, amount));
        }
        [Fact]
        public void AddToShoppingCartTest(int amount)
        {
            Order order = new Order();
            ShoesDAL shoesDAL = new ShoesDAL();
            CustomerDAL customerDAL = new CustomerDAL();
            order.OrderUser = new Customer();
            Shoes shoes = new Shoes();
            order.OrderItem = new Shoes();


            order.OrderItem = shoesDAL.GetShoesById(9);
            order.OrderUser = customerDAL.GetCustomerByID(1);


            MySqlCommand command = DBHelper.OpenConnection().CreateCommand();
            command.CommandText = $"insert into Orders(order_customer,order_status) values ({order.OrderUser.UserID},0)";
            command.ExecuteNonQuery();
            customerDAL.UpdateStatusShoppingCartById(false, order.OrderUser.UserID); // set userShopping cart to 1

            Assert.True(orderDAL.AddToShoppingcart(order, amount));

            orderDAL.DeleteAllItemInShoppingCartByUserID(order.OrderUser.UserID);
            customerDAL.UpdateStatusShoppingCartById(true, order.OrderUser.UserID); // set userShopping cart to 0
        }
        [Fact]
        public void AddToShoppingCartTest1(int amount)
        {
            Order order = new Order();
            ShoesDAL shoesDAL = new ShoesDAL();
            order.OrderUser = new Customer();
            Shoes shoes = new Shoes();
            order.OrderItem = new Shoes();
            CustomerDAL customerDAL = new CustomerDAL();


            order.OrderUser.UserID = 0;
            order.OrderItem.ShoesId = 0;


            Assert.False(orderDAL.AddToShoppingcart(order,amount));
        }
        [Fact]
        public void AddToShoppingCartTest2(int amount)
        {
            Order order = new Order();
            CustomerDAL customerDAL = new CustomerDAL();
            order.OrderUser = new Customer();
            Shoes item = new Shoes();
            order.OrderItem = new Shoes();


            order.OrderUser.UserID = 1;
            order.OrderItem.ShoesId = null;


            MySqlCommand command = DBHelper.OpenConnection().CreateCommand();
            command.CommandText = $"insert into Orders(order_customer,order_status) values ({order.OrderUser.UserID},0)";
            command.ExecuteNonQuery();
            customerDAL.UpdateStatusShoppingCartById(false, order.OrderUser.UserID); // set userShopping cart to 1

            Assert.False(orderDAL.AddToShoppingcart(order, amount));

            orderDAL.DeleteAllItemInShoppingCartByUserID(order.OrderUser.UserID);
            customerDAL.UpdateStatusShoppingCartById(true, order.OrderUser.UserID); // set userShopping cart to 0
        }
        [Fact]
        public void DeleteItemInShoppingCartByIdItemTest()
        {
            int userId = 1;
            int idItem = 1;
            MySqlCommand command = DBHelper.OpenConnection().CreateCommand();
            command.CommandText = $"insert into Orders(order_customer,order_status) values ({userId},0)";
            command.ExecuteNonQuery();
            int orderID = GetLastInsertOrderID(1);
            command.CommandText = $"insert into OrderDetails(order_id,item_id) values ({orderID},{idItem})";
            command.ExecuteNonQuery();

            Assert.True(orderDAL.DeleteItemInShoppingCartByIdItem(userId));
            orderDAL.DeleteAllItemInShoppingCartByUserID(userId);
        }
        [Fact]
        public void DeleteItemInShoppingCartByIdItemTest1()
        {

            Assert.False(orderDAL.DeleteItemInShoppingCartByIdItem(null));

        }
        [Fact]
        public void DeleteAllItemInShoppingCartByUserIDTest()
        {
            int userId = 1;
            MySqlCommand command = DBHelper.OpenConnection().CreateCommand();
            command.CommandText = $"insert into Orders(order_customer,order_status) values ({userId},0)";
            command.ExecuteNonQuery();
            int orderId = GetLastInsertOrderID(1);
            command.CommandText = $"insert into OrderDetails(order_id,item_id) values ({orderId},1)";
            command.ExecuteNonQuery();

            Assert.True(orderDAL.DeleteAllItemInShoppingCartByUserID(userId));
        }
        [Fact]
        public void DeleteAllItemInShoppingCartByUserIDTest1()
        {

            Assert.False(orderDAL.DeleteAllItemInShoppingCartByUserID(0));
        }

        [Fact]
        public void ShowShopingCartByUserIdTest()
        {
            Assert.NotNull(orderDAL.ShowShopingCartByUserId(1));
        }
        [Fact]
        public void ShowShopingCartByUserIdTest1()
        {
            Assert.Null(orderDAL.ShowShopingCartByUserId(null));
        }
        [Fact]
        public void CreateOrderTest(int amount)
        {
            CustomerDAL customerDAL = new CustomerDAL();
            ShoesDAL shoesDAL = new ShoesDAL();
            Order order = new Order();
            order.OrderUser = new Customer();
            order.OrderItem = new Shoes();

            order.OrderStatus = 0;
            order.OrderItem = shoesDAL.GetShoesById(2);
            order.OrderUser = customerDAL.GetCustomerByID(1);
            orderDAL.CreateShoppingCart(order,amount);

            Assert.True(orderDAL.CreateOrder(order));

            orderDAL.DeleteAllItemInShoppingCartByUserID(1);
        }

        [Fact]
        public void ShowOrderByUserIdTest(int amount)
        {
            CustomerDAL customerDAL = new CustomerDAL();
            ShoesDAL shoesDAL = new ShoesDAL();
            Order order = new Order();
            order.OrderUser = new Customer();
            order.OrderItem = new Shoes();

            order.OrderStatus = 0;
            order.OrderItem = shoesDAL.GetShoesById(2);
            order.OrderUser = customerDAL.GetCustomerByID(1);
            orderDAL.CreateShoppingCart(order,amount);
            orderDAL.CreateOrder(order);

            Assert.NotNull(orderDAL.ShowAllItemOrdered(1));

            orderDAL.DeleteAllItemInShoppingCartByUserID(1);
        }
        [Fact]
        public void ShowOrderByUserIdTest1()
        {
            Assert.Null(orderDAL.ShowAllItemOrdered(null));
        }
        [Fact]
        public void ShowOrderUserPaySucessTest(int amount)
        {
            CustomerDAL customerDAL = new CustomerDAL();
            ShoesDAL shoesDAL = new ShoesDAL();
            Order order = new Order();
            order.OrderUser = new Customer();
            order.OrderItem = new Shoes();

            order.OrderStatus = 0;
            order.OrderItem = shoesDAL.GetShoesById(2);
            order.OrderUser = customerDAL.GetCustomerByID(1);
            orderDAL.CreateShoppingCart(order, amount);
            orderDAL.CreateOrder(order);

            Assert.NotNull(orderDAL.ShowOrderUserPaySucess(1));

            orderDAL.DeleteAllItemInShoppingCartByUserID(1);
        }
        [Fact]
        public void ShowOrderUserPaySucessTest1()
        {
            Assert.Null(orderDAL.ShowOrderUserPaySucess(null));
        }
        public int GetLastInsertOrderID(int userID)
        {
            int orderId = -1;

            string queryLastInsertId = $@"select order_id from orders where order_customer = {userID} order by orderid desc limit 1;";
            reader = DBHelper.ExecQuery(queryLastInsertId, DBHelper.OpenConnection());
            if (reader.Read())
            {
                orderId = reader.GetInt32("orderId");

            }
            reader.Close();
            return orderId;
        }
    }
}
