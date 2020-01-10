// using System;
// using Xunit;
// using Persistence;
// using System.Collections.Generic;
// using MySql.Data.MySqlClient;
// using BL;
// namespace DAL.Test
// {

//     public class OrderUnitTest
//     {
//         private MySqlDataReader reader;
//         private OrderBL orderBL = new OrderBL();
//         private OrderDAL orderDAL = new OrderDAL();
//         [Fact]
//         public void CreateShoppingCartTest()
//         {
//             CustomerBL customerBL = new CustomerBL();
//             ShoesBL shoesBL = new ShoesBL(); 
//             Order order = new Order();
//             order.OrderUser = new Customer();
//             order.OrderItem = new Shoes();

//             order.OrderStatus = 0;
//             order.OrderItem = shoesBL.GetShoesById(2);
//             order.OrderUser = customerBL.GetCustomerByID(1);
//             Assert.True(orderBL.CreateShoppingCart(order));
//             orderBL.DeleteAllItemInShoppingCartByUserID(1);
//         }
//         [Fact]
//         public void CreateShoppingCartTest1()
//         {
//              CustomerBL customerBL = new CustomerBL();
//             ShoesBL shoesBL = new ShoesBL(); 
//             Order order = new Order();
//             order.OrderUser = new Customer();
//             order.OrderItem = new Shoes();

//             order.OrderStatus = 0;
//             order.OrderItem = shoesBL.GetShoesById(0);
//             order.OrderUser = customerBL.GetCustomerByID(0);

//             Assert.False(orderBL.CreateShoppingCart(order));
//         }
//         [Fact]
//         public void AddToShoppingCartTest()
//         {
//             Order order = new Order();
//             CustomerDAL customerDAL = new CustomerDAL();
//             order.OrderUser = new Customer();
//             Shoes shoes = new Shoes();
//             order.OrderItem = new Shoes();
//             order.OrderUser.UserID = 1;
//             order.OrderItem.ShoesId = 4;


//             MySqlCommand command = DBHelper.OpenConnection().CreateCommand();
//             command.CommandText = $"insert into Orders(order_customer,order_status) values ({order.OrderUser.UserID},0)";
//             command.ExecuteNonQuery();
//             customerDAL.UpdateStatusShoppingCartById(false, order.OrderUser.UserID); // set userShopping cart to 1

//             Assert.True(orderBL.AddToShoppingcart(order));

//             orderBL.DeleteAllItemInShoppingCartByUserID(order.OrderUser.UserID);
//             customerDAL.UpdateStatusShoppingCartById(true, order.OrderUser.UserID); // set userShopping cart to 0
//         }
//         [Fact]
//         public void AddToShoppingCartTest1()
//         {
//             Order order = new Order();
//             order.OrderUser = new Customer();
//             Shoes shoes = new Shoes();
//             order.OrderItem = new Shoes();


//             order.OrderUser.UserID = 0;
//             order.OrderItem.ShoesId = 0;

//             Assert.False(orderBL.AddToShoppingcart(order));
//         }
//         [Fact]
//         public void AddToShoppingCartTest2()
//         {
//             Order order = new Order();
//             CustomerDAL customerDAL = new CustomerDAL();
//             order.OrderUser = new Customer();
//             Shoes shoes = new Shoes();
//             order.OrderItem = new Shoes();


//             order.OrderUser.UserID = 1;
//             order.OrderItem.ShoesId = null;


//             MySqlCommand command = DBHelper.OpenConnection().CreateCommand();
//             command.CommandText = $"insert into Orders(order_customer,order_status) values ({order.OrderUser.UserID},0)";
//             command.ExecuteNonQuery();
//             customerDAL.UpdateStatusShoppingCartById(false, order.OrderUser.UserID); // set userShopping cart to 1

//             Assert.False(orderBL.AddToShoppingcart(order));

//             orderBL.DeleteAllItemInShoppingCartByUserID(order.OrderUser.UserID);
//             customerDAL.UpdateStatusShoppingCartById(true, order.OrderUser.UserID); // set userShopping cart to 0
//         }
//         [Fact]
//         public void DeleteItemInShoppingCartByIdItemTest()
//         {
//             int userId = 1;
//             int idItem = 1;
//             MySqlCommand command = DBHelper.OpenConnection().CreateCommand();
//             command.CommandText = $"insert into Orders(order_customer,order_status) values ({userId},0)";
//             command.ExecuteNonQuery();
//             int orderID = GetLastInsertOrderID(1);
//             command.CommandText = $"insert into OrderDetails(order_id,item_id) values ({orderID},{idItem})";
//             command.ExecuteNonQuery();

//             Assert.True(orderBL.DeleteItemInShoppingCartByIdItem(userId));
//             orderBL.DeleteAllItemInShoppingCartByUserID(userId);
//         }
//         [Fact]
//         public void DeleteItemInShoppingCartByIdItemTest1()
//         {

//             Assert.False(orderBL.DeleteItemInShoppingCartByIdItem(null));

//         }
//         [Fact]
//         public void DeleteAllItemInShoppingCartByUserIDTest()
//         {
//             int userId = 1;
//             MySqlCommand command = DBHelper.OpenConnection().CreateCommand();
//             command.CommandText = $"insert into Orders(order_customer,order_status) values ({userId},0)";
//             command.ExecuteNonQuery();
//             int orderID = GetLastInsertOrderID(1);
//             command.CommandText = $"insert into OrderDetails(order_id,item_id) values ({orderID},1)";
//             command.ExecuteNonQuery();

//             Assert.True(orderBL.DeleteAllItemInShoppingCartByUserID(userId));
//         }
//         [Fact]
//         public void DeleteAllItemInShoppingCartByUserIDTest1()
//         {

//             Assert.False(orderBL.DeleteAllItemInShoppingCartByUserID(0));
//         }

//         [Fact]
//         public void ShowShopingCartByUserIdTest()
//         {
//             Assert.NotNull(orderBL.ShowShopingCartByUserId(1));
//         }
//         [Fact]
//         public void ShowShopingCartByUserIdTest1()
//         {
//             Assert.Null(orderBL.ShowShopingCartByUserId(null));
//         }
//         [Fact]
//         public void CreateOrderTest()
//         {
//             CustomerDAL customerDAL = new CustomerDAL();
//             ShoesDAL shoesDAL = new ShoesDAL();
//             Order order = new Order();
//             order.OrderUser = new Customer();
//             order.OrderItem = new Shoes();

//             order.OrderStatus = 0;
//             order.OrderItem = shoesDAL.GetShoesById(2);
//             order.OrderUser = customerDAL.GetCustomerByID(1);
//             orderDAL.CreateShoppingCart(order);

//             Assert.True(orderBL.CreateOrder(order));

//             orderBL.DeleteAllItemInShoppingCartByUserID(1);


//         }

//         [Fact]
//         public void ShowOrderByUserIdTest()
//         {
//             CustomerDAL customerDAL = new CustomerDAL();
//             ShoesDAL shoesDAL = new ShoesDAL();
//             Order order = new Order();
//             order.OrderUser = new Customer();
//             order.OrderItem = new Shoes();

//             order.OrderStatus = 0;
//             order.OrderItem = shoesDAL.GetShoesById(2);
//             order.OrderUser = customerDAL.GetCustomerByID(1);
//             orderBL.CreateShoppingCart(order);
//             orderBL.CreateOrder(order);

//             Assert.NotNull(orderBL.ShowAllItemOrdered(1));

//             orderBL.DeleteAllItemInShoppingCartByUserID(1);
//         }
//         [Fact]
//         public void ShowOrderByUserIdTest1()
//         {
//             Assert.Null(orderBL.ShowAllItemOrdered(null));
//         }
//         [Fact]
//         public void ShowOrderUserPaySucessTest()
//         {
//             CustomerDAL customerDAL = new CustomerDAL();
//             ShoesDAL shoesDAL = new ShoesDAL();
//             Order order = new Order();
//             order.OrderUser = new Customer();
//             order.OrderItem = new Shoes();

//             order.OrderStatus = 0;
//             order.OrderItem = shoesDAL.GetShoesById(2);
//             order.OrderUser = customerDAL.GetCustomerByID(1);

//             orderBL.CreateShoppingCart(order);
//             orderBL.CreateOrder(order);

//             Assert.NotNull(orderBL.ShowOrderUserPaySucess(1));

//             orderBL.DeleteAllItemInShoppingCartByUserID(1);
//         }
//         [Fact]
//         public void ShowOrderUserPaySucessTest1()
//         {
//             Assert.Null(orderBL.ShowOrderUserPaySucess(null));
//         }
//         public int GetLastInsertOrderID(int userID)
//         {
//             int orderId = -1;

//             string queryLastInsertId = $@"select order_id from orders where order_customer = {userID} order by order_id desc limit 1;";
//             reader = DBHelper.ExecQuery(queryLastInsertId, DBHelper.OpenConnection());
//             if (reader.Read())
//             {
//                 orderId = reader.GetInt32("orderId");

//             }
//             reader.Close();
//             return orderId;
//         }
//     }
// }
