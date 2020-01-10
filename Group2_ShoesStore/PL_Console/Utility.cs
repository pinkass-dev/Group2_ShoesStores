using System;
using Persistence;
using BL;
using ConsoleTables;
using System.Text;
using System.Collections.Generic;
using System.Globalization;
namespace PL_Console
{
    public class Utility
    {
        private static string row1 = "=============================================";
        private static string row2 = "---------------------------------------------";

        public static short MenuTemplate(string title, string[] menuItems)
        {
            Console.Clear();
            short choose = 0;
            Console.WriteLine(row1);
            Console.WriteLine(title);
            Console.WriteLine(row2);
            for (int i = 0; i < menuItems.Length; i++)
            {
                Console.WriteLine((i + 1) + ". " + menuItems[i]);
            }
            Console.WriteLine(row1);
            try
            {
                Console.WriteLine("#Select: ");
                choose = Int16.Parse(Console.ReadLine());
            }
            catch (System.Exception)
            {


            }
            if (choose <= 0 || choose > menuItems.Length)
            {
                do
                {
                    try
                    {
                        Console.Write("Please enter the correct selection!");
                        choose = Int16.Parse(Console.ReadLine());
                    }
                    catch (System.Exception)
                    {
                        continue;

                    }
                } while (choose <= 0 || choose > menuItems.Length);
            }
            return choose;
        }
        public static short MenuDetail(string title, string[] menuItems)
        {

            short choose = 0;

            for (int i = 0; i < menuItems.Length; i++)
            {
                Console.WriteLine((i + 1) + ". " + menuItems[i]);
            }
            Console.WriteLine(row1);
            try
            {
                Console.Write("#Select: ");
                choose = Int16.Parse(Console.ReadLine());
            }
            catch (System.Exception)
            {

            }
            if (choose <= 0 || choose > menuItems.Length)
            {
                do
                {
                    try
                    {
                        Console.Write("#Please enter the correct selection: ");
                        choose = Int16.Parse(Console.ReadLine());
                    }
                    catch (System.Exception)
                    {
                        continue;
                    }
                } while (choose <= 0 || choose > menuItems.Length);
            }
            return choose;
        }
        public static short showListItems(string title, string[] menuItems, List<Shoes> shoes, int UserID)

        {
            Console.Clear();
            Console.Clear();
            short choice = -1;
            var table = new ConsoleTable("Shoes ID", "Name", "Price", "Size", "Color", "Material", "Brand","Quantity");

            OrderBL orderBL = new OrderBL();

            foreach (Shoes item in shoes)
            {
                table.AddRow(item.ShoesId, item.ShoesName, FormatCurrency(item.ShoesPrice) , item.ShoesSize, item.ShoesColor, item.ShoesMaterial, item.ShoesBrand, item.ShoesQuantity);

            }
            table.Write();
            if (shoes.Count <= 0)
            {
                Console.WriteLine("No shoes information found");
            }
            ShoesBL shoesBL = new ShoesBL();


            for (int i = 0; i < menuItems.Length; i++)
            {
                Console.WriteLine((i + 1) + ". " + menuItems[i]);
            }

            Console.WriteLine(row1);
            try
            {
                Console.Write("#Select: ");
                choice = Int16.Parse(Console.ReadLine());
            }
            catch (System.Exception)
            {

            }
            if (choice < 0 || choice > menuItems.Length)
            {
                do
                {
                    try
                    {
                        Console.Write("#Please enter the correct selection ");
                        choice = Int16.Parse(Console.ReadLine());
                    }
                    catch (System.Exception)
                    {
                        continue;
                    }
                } while (choice < 0 || choice > menuItems.Length);
            }
            return choice;

        }
        public static void InfoCustomer(string title, string[] menuItems, Customer cus)
        {
            Console.Clear();
            Console.WriteLine(row1);
            Console.WriteLine(title);
            // Console.WriteLine(row2);

            string[] infoUser = { cus.UserAccount, cus.UserName, cus.UserEmail, 
            cus.UserAdd,cus.UserPhone,cus.UserGender,cus.UserBirthDay };
            var table = new ConsoleTable("Account", cus.UserAccount);
            table.AddRow("Name", cus.UserName);
            table.AddRow("Email", cus.UserEmail);
            table.AddRow("Address", cus.UserAdd);
            table.AddRow("Phone Number", cus.UserPhone);
            table.AddRow("Gender",cus.UserGender);
            table.AddRow("Date Of Birth", cus.UserBirthDay);
            table.Write();
            Console.WriteLine(row1);
            Console.Write("Press any key to return! ");
            Console.ReadKey();

        }
        public static short SelectAnItem(List<Shoes> shoes)

        {

            short idItem = -1;
             string shoesSize = "";
            bool isHave = false;
            try
            {
                Console.Write("Please enter Shoes's ID: ");
                idItem = Int16.Parse(Console.ReadLine());
                Console.Write("Please enter Shoes' Size: ");
                shoesSize = Convert.ToString(Console.ReadLine());
            }
            catch (System.Exception)
            {

            }
            foreach (var item in shoes)
            {

                if (idItem == item.ShoesId)
                {
                    isHave = true;
                }
            }
            foreach (var item in shoes)
            {
                if (shoesSize == item.ShoesSize)
                {
                    isHave = true;
                }
            }
            if (!isHave)
            {
                do
                {
                    try
                    {
                        Console.Write("#Please enter Shoes' Id again: ");
                        idItem = Int16.Parse(Console.ReadLine());
                        Console.Write(" Shoes'Size: ");
                        shoesSize = Convert.ToString(Console.ReadLine());
                        foreach (var item in shoes)
                        {

                            if (idItem == item.ShoesId)
                            {
                                isHave = true;
                            }
                        }
                        foreach (var item in shoes)
                        {
                            if (shoesSize == item.ShoesSize)
                            {
                                isHave = true;
                            }
                        }
                    }
                    catch (System.Exception)
                    {
                        continue;
                    }
                } while (!isHave);
            }
            return idItem;


        }
        public static string OnlyYN(string printcl)
        {
            string choice;
            Console.Write(printcl);
            choice = Console.ReadLine().ToUpper();
            while (true)
            {
                if (choice != "Y" && choice != "N")
                {
                    Console.Write("Enter YES or NO (Y/N): ");
                    choice = Console.ReadLine().ToUpper();
                    continue;
                }
                break;
            }
            return choice;
        }
        public static string FormatCurrency(double price)
        {
            string a = string.Format(new CultureInfo("vi-VN"), "{0:#,##0} VND", price);
            return a;
        }
    }
}