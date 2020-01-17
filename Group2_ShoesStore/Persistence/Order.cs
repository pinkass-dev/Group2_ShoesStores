using System;
using System.Collections.Generic;

namespace Persistence
{
    public class Order
    {
        public Order(){}
        public Order(int? orderId, Customer orderUser,DateTime? orderDate, int orderStatus, Shoes orderItem,List<Shoes> listShoes)
        {
            OrderId = orderId;
            OrderUser = orderUser;
            OrderDate = orderDate;
            OrderStatus = orderStatus;
            OrderItem = orderItem;
            ListShoes = listShoes;
        }
        
        public int? OrderId {get;set;}
        public Customer OrderUser{get;set;}
        public DateTime? OrderDate{get;set;}
        public int OrderStatus{get;set;}
        public Shoes OrderItem{get;set;}
        public List<Shoes> ListShoes;
        public int Amount{get;set;}
        public double Price {get; set;} 
        public double Total {get; set;}
    }
    
}