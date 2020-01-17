using System;
using DAL;
using System.Collections.Generic;
using Persistence;

namespace BL
{
    public class OrderBL
    {
        OrderDAL orderDAL;
        public OrderBL()
        {
            orderDAL = new OrderDAL();
        }
        public bool CreateShoppingCart(Order order, int amount)
        {

            if (order == null)
            {
                return false;
            }
            return orderDAL.CreateShoppingCart(order, amount);
        }
        public bool AddToShoppingcart(Order order, int amount)
        {

            return orderDAL.AddToShoppingcart(order, amount);
        }
        public bool DeleteItemInShoppingCartByIdItem(int? ShoesId)
        {

            return orderDAL.DeleteItemInShoppingCartByIdItem(ShoesId);
        }
        public List<Shoes> ShowShopingCartByUserId(int? userId)
        {
            return orderDAL.ShowShopingCartByUserId(userId);
        }
        public bool CreateOrder(Order order)
        {
            return orderDAL.CreateOrder(order);
        }
        public List<Order> ShowAllItemOrdered(int? userId)
        {
            return orderDAL.ShowAllItemOrdered(userId);
        }
        public List<Order> ShowOrderUserPaySucess(int? userId)
        {
            if (userId == null)
            {
                return null;
            }
            List<Order> listOrders = orderDAL.ShowOrderUserPaySucess(userId);
            foreach (var item in listOrders)
            {
                item.OrderItem.ShoesName.ToUpper();
            }
            return listOrders;
        }
         public bool DeleteAllItemInShoppingCartByUserID(int? UserID)
        {

            return orderDAL.DeleteAllItemInShoppingCartByUserID(UserID);
        }
        // public int? CheckItemPurchase(int? itemId, int? userId)
        // {

        //     return orderDAL.CheckItemPurchase(itemId, userId);
        // }
    }

}