using System;
using Persistence;
using BL;
using System.Collections.Generic;
using ConsoleTables;
using System.Text.RegularExpressions;
using System.Text;
using System.Globalization;
using System.Linq;

namespace PL_Console
{

    public class ConsoleCustomer
    {
        private ShoesBL shoesBL = new ShoesBL();
        private CustomerBL customerBL = new CustomerBL();
        private Customer customer = new Customer();
        private Order order = new Order();
        public void MenuCustomer(Customer cus)
        {

            customer = cus;
            while (true)
            {
                OrderBL orderBL = new OrderBL();
                string[] choice = {"Account Information", "List Shoes",
                 "View Cart", "Exit"};
                short choose = Utility.MenuTemplate("Menu", choice);
                switch (choose)
                {
                    case 1:
                        ShowInfoCustomer(cus);
                        continue;
                    case 2:
                        ShowlistItems();
                        continue;
                    case 3:
                        ShopingCart();
                        continue;
                    // case 4:
                    //     ShowOrder();
                    //     continue;
                }
                break;

            }

        }
        public void ShowInfoCustomer(Customer cus)
        {

            string[] listcol = { "Accout", "Name", "Email", "Address", "Phonenumber", "Gender", "Date Of Birth" };

            Utility.InfoCustomer("Account Information", listcol, cus);

        }
        public void ShowlistItems()
        {

            Console.Clear();
            List<Shoes> shoes = null;

            shoes = shoesBL.GetListShoes(1);
            if (shoes == null)
            {
                Console.ReadKey();
            }
            else
            {
                while (true)
                {


                    // int? idItem;
                    string[] listcol = { "Select Product", "Search", "Enter Page", "Back" };
                    int choice = Utility.showListItems("List Shoes", listcol, shoes, customer.UserID);
                    switch (choice)
                    {
                        case 1:
                            if (shoes.Count <= 0)
                            {
                                Console.WriteLine("No Shoes");
                                Console.WriteLine("Press any key to display the shoe list");
                                Console.ReadKey();
                                shoes = shoesBL.GetListShoes(1);
                            }
                            else
                            {
                                
                                var stringTest = Utility.SelectAnItem(shoes);
                                string[] tokens = stringTest.Split(',');
                                var id = Convert.ToInt32(tokens[1]);
                                
                                var amount = Convert.ToInt32(tokens[0]);
                                
                                
                                ShowAnItem(id,amount);
                            }
                            continue;
                        case 2:
                            Console.Write("Enter Shoes' Name: ");
                            Console.InputEncoding = Encoding.Unicode;
                            Console.OutputEncoding = Encoding.Unicode;
                            string itemName = Console.ReadLine();
                            shoes = shoesBL.SearchShoesName(itemName);
                            continue;

                        case 3:
                            Console.Write(" Enter Page:");
                            var pageIndex = Convert.ToInt32(Console.ReadLine());
                            shoes = shoesBL.GetListShoes(pageIndex);
                            continue;
                        case 4:
                            break;

                    }
                    break;
                }
            }


        }
        public void ShowAnItem(int? itemId, int amount)
        {
            while (true)
            {
                Console.Clear();
                Console.Clear();
                var shoeList = new List<Shoes>();
                Shoes shoes = new Shoes();
                shoes = shoesBL.GetShoesById(itemId);
                var table = new ConsoleTable("Name:", Convert.ToString(shoes.ShoesName));
                table.AddRow("Size:", SubStringSize(shoes.ShoesSize));
                table.AddRow("Price:", FormatCurrency(shoes.ShoesPrice));
                table.AddRow("Color:", shoes.ShoesColor);
                table.AddRow("Material:", shoes.ShoesMaterial);
                table.AddRow("Brand:", shoes.ShoesBrand);
                table.Write();
                Console.WriteLine();
                OrderBL orderBL = new OrderBL();
                string[] choice = { "Add To Cart", "Back" };
                short choose = Utility.MenuDetail("Menu", choice);
                switch (choose)
                {
                    case 1:
                        AddToCart(shoes, amount);
                        
                        break;
                }
                break;
            }
        }
        public void AddToCart(Shoes shoes, int amount)
        {

            OrderBL orderBL = new OrderBL();
            order.OrderUser = new Customer();
            order.OrderItem = new Shoes();
            order.ListShoes = new List<Shoes>();
            order.OrderUser.UserID = customer.UserID;
            order.OrderItem.ShoesId = shoes.ShoesId;
            if (customerBL.GetCustomerByID(customer.UserID).UserShoppingCart)
            {
                try
                {
                    if (orderBL.AddToShoppingcart(order, amount))
                    {
                        Console.WriteLine("Add to cart successfully");
                    }
                    else
                    {
                        Console.WriteLine("Products already in the cart");
                    }
                }
                catch (System.Exception)
                {
                    Console.WriteLine("XXX");
                    throw;
                }

            }
            else
            {
                // Console.WriteLine(customer.UserID);
                customerBL.UpdateStatusShoppingCartById(false, customer.UserID); // set userShopping cart to 1
                order.OrderStatus = 0;
                try
                {
                    if (orderBL.CreateShoppingCart(order, amount))
                    {
                        Console.WriteLine("Add to cart successfully");
                    }
                }
                catch (System.Exception)
                {
                    Console.WriteLine("XXX1");
                    throw;
                }

            }
            Console.WriteLine("press any key to continue");
            Console.ReadKey();
        }
        public void DeleteItemFromSPC(Shoes shoes)
        {
            OrderBL orderBL = new OrderBL();
            if (orderBL.DeleteItemInShoppingCartByIdItem(shoes.ShoesId))
            {
                Console.WriteLine("Delete from cart");
            }
            else
            {
                Console.WriteLine("The product is not in the cart");
            }
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }
        public void ShopingCart()
        {
            while (true)
            {
                Console.Clear();
                OrderBL orderBL = new OrderBL();
                List<Shoes> shoppingCart = new List<Shoes>();
                shoppingCart = orderBL.ShowShopingCartByUserId(customer.UserID);
                if (shoppingCart == null)
                {
                    Console.ReadKey();
                    break;
                }
                double total = 0;
                if (shoppingCart.Count <= 0)
                {
                    Console.WriteLine("No shoes yet");
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                    break;
                }
                else
                {
                    // Console.WriteLine($"You have {shoppingCart.Count} shoes in cart");
                    var table = new ConsoleTable("ID", "NAME", "PRICE");
                    foreach (var item in shoppingCart)
                    {
                        total = total + (double)item.ShoesPrice;
                        table.AddRow(item.ShoesId, item.ShoesName, FormatCurrency(item.ShoesPrice));
                    }
                    table.AddRow("", "", "");
                    table.AddRow("AMOUNT","", shoppingCart[0].aMount);
                    table.AddRow("TOTAL", "", FormatCurrency(shoppingCart[0].toTal));
                    table.Write();
                    // Console.WriteLine("Total: {0}", FormatCurrency(total));

                    Console.WriteLine();
                    string[] choice = { "Pay", "Delete Shoes from cart", "Back" };
                    short choose = Utility.MenuDetail("Menu", choice);
                    string yorn;
                    switch (choose)
                    {
                        case 1:
                            yorn = Utility.OnlyYN("Do you want to pay?(Y/N) ");
                            if (yorn == "Y")
                            {
                                CreateOrder(total);
                            }
                            continue;
                        case 2:
                            Console.Write("Enter Shoes' Id to delete: ");  
                            int idItem;
                            bool c = Int32.TryParse(Console.ReadLine(), out idItem);
                            if (!c)
                            {
                                Console.WriteLine("You can only enter numbers. Press any button to return");
                                Console.ReadKey();
                                continue;
                            }

                            bool y = false;
                            foreach (var item in shoppingCart)
                            {
                                if (item.ShoesId == idItem)
                                {
                                    y = true;
                                }
                            }
                            if (y)
                            {

                                string yOrN = Utility.OnlyYN("Do you really want to delete?(Y/N): ");
                                if (yOrN == "Y")
                                {
                                    orderBL.DeleteItemInShoppingCartByIdItem(idItem);
                                }

                            }
                            else
                            {
                                Console.WriteLine("There are no shoes in this cart");
                                Console.WriteLine("Press any key to continue");
                                Console.ReadKey();
                            }

                            continue;
                    }
                    break;
                }

            }
        }
       
        public void CreateOrder(double total)
        {
            order.OrderUser = new Customer();
            OrderBL orderBL = new OrderBL();
            order.OrderUser = customer;
            try
            {
                if (orderBL.CreateOrder(order))
                {
                    Console.Clear();
                    customerBL.UpdateStatusShoppingCartById(true, customer.UserID);
                    Console.WriteLine("Shoes Stores");
                    List<Order> shoppingCart = new List<Order>();
                    shoppingCart = orderBL.ShowOrderUserPaySucess(customer.UserID);
                    Console.WriteLine("INVOICE");
                    Console.WriteLine("CUSTOMER'S NAME: {0}", shoppingCart[0].OrderUser.UserName);
                    Console.WriteLine("CUSTOMER'S EMAIL: {0}", shoppingCart[0].OrderUser.UserEmail);
                    Console.WriteLine("CODE ORDERS: {0}", shoppingCart[0].OrderId);                 
                    var tables = new List<ConsoleTable>();
                    var table = new ConsoleTable("ID", "NAME", "PRICE");
                    foreach (var item in shoppingCart)
                    {
                        table.AddRow(item.OrderItem.ShoesId, item.OrderItem.ShoesName, FormatCurrency(item.OrderItem.ShoesPrice));
                        // tables.Add(table);
                    }
                    table.AddRow("", "", ""); 
                    table.AddRow("AMOUNT", "", shoppingCart[0].Amount);
                    table.AddRow("TOTAL AMOUNT", "", FormatCurrency(shoppingCart[0].Total));
                    table.AddRow("DATE", "", shoppingCart[0].OrderDate?.ToString("yyyy-MM-dd"));
                    tables.Add(table);
                    foreach (var item in tables)
                    {
                        item.Write();
                    }
                    Console.WriteLine("THANKS YOU");
                }
                else
                {
                    Console.WriteLine("Purchase failed");
                }
            }
            catch (System.Exception)
            {
                Console.WriteLine("NO MONEY????");
                throw;
            }


            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }
        public void ShowOrder()
        {
            Console.Clear();
            OrderBL orderBL = new OrderBL();
            List<Order> listOrder = new List<Order>();
            listOrder = orderBL.ShowAllItemOrdered(customer.UserID);
            if (listOrder == null)
            {
                Console.ReadKey();
            }
            else
            {
                if (listOrder.Count <= 0)
                {
                    Console.WriteLine("You have not purchased anything");
                }
                else
                {
                    var table = new ConsoleTable("SHOES CODE", "SHOES NAME", "DATE");
                    foreach (var item in listOrder)
                    {
                        table.AddRow(item.OrderItem.ShoesId, item.OrderItem.ShoesName, item.OrderDate?.ToString("yyyy-MM-dd"));
                    }
                    table.Write();
                }
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
            }
        }

        public string FormatCurrency(double price)
        {
            string a = string.Format(new CultureInfo("vi-VN"), "{0:#,##0} VNĐ", price);
            return a;
        }
        public string SubStringSize(string ShoesSize)
        {
            string b = ShoesSize.Substring(0, 2);
            return b;
        }

    }
}


