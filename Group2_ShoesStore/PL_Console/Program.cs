using System;
using System.Collections.Generic;
using BL;
using Persistence;
using ConsoleTables;

namespace PL_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            // // public void ShowShoesDetails()
            // // {
            // string ShoesName;
            // Console.Write("Please enter an shoes' name: ");
            // ShoesName = Convert.ToString(Console.ReadLine());
            // ShoesBL shoesBL = new ShoesBL();
            // Shoes sh = shoesBL.GetShoesByName(ShoesName);
            // if (sh != null)
            // {
            //     Console.WriteLine("Shoes Name: " + sh.ShoesName);
            //     Console.WriteLine("Shoes Price: " + sh.ShoesPrice);
            //     Console.WriteLine("Shoes Quantity: " + sh.ShoesQuantity);
            //     Console.WriteLine("Shoes Size: " + sh.ShoesSize);
            //     Console.WriteLine("Shoes Color: " + sh.ShoesColor);
            //     Console.WriteLine("Shoes Material" + sh.ShoesMaterial);
            //     Console.WriteLine("Shoes Brand: " + sh.ShoesBrand);
            //  }
            //  else
            //  {
            //      System.Console.WriteLine("Shoes Name not exist!");
            //  }

            // // }

            ShoesBL shoesBL = new ShoesBL();
            List<Shoes> listshoes = null;
            // List<Shoes> listshoes = shoesBL.GetListShoes();
            listshoes = shoesBL.GetListShoes();
            var table = new ConsoleTable("ShoesID", "Name", "Price", "Quantity", "Size", "Color", "Material", "Brand");
            foreach (var item in listshoes)
            {
                table.AddRow(item.ShoesId, item.ShoesName, item.ShoesPrice, item.ShoesQuantity, item.ShoesSize, item.ShoesColor, item.ShoesMaterial, item.ShoesBrand);

            }
            table.Write(Format.Alternative);
        }
    }
}
