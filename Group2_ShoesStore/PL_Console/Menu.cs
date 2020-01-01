using System;
using Persistence;
using BL;
using System.Text;
using System.Text.RegularExpressions;
using ConsoleTables;
namespace PL_Console
{
    public class Menu
    {
        public void Program()
        {
            while (true)
            {

                string[] choice = { "Login", "Exit" };
                // short choose = Mainmenu("EBook Store", choice);
                short choose = Utility.MenuTemplate("Shoes Stores", choice);
                switch (choose)
                {
                    case 1:
                        MenuLogin();
                        continue;
                    case 2:
                        
                        string yOrN = Utility.OnlyYN("Do you want to exit?(Y/N): ");
                        if (yOrN == "Y")
                        {
                            Environment.Exit(0);
                            break;
                        }
                        continue;

                }
            }

        }
        public void MenuLogin()
        {

            CustomerBL customerBL = new CustomerBL();
            string username = null;
            string password = null;
            string choice;
            while (true)
            {
                Console.Clear();
                Customer customer = null;
                string row1 = "=====================================================================";
                string row2 = "---------------------------------------------------------------------";
                Console.WriteLine(row1);
                Console.WriteLine(" LOGIN");
                Console.WriteLine(row2);
                Console.Write("Username: ");
                username = Console.ReadLine();
                Console.Write("Password: ");
                password = Password();

                if (ValidateLogin(username) == false || ValidateLogin(password) == false)
                {
                    Console.WriteLine("Username or password cannot contain special characters");
                    choice = Utility.OnlyYN("Do you want to continue? Y/N: ");
                    switch (choice)
                    {
                        case "Y":
                            continue;
                        case "N":
                            Program();
                            break;
                        default:
                            continue;
                    }
                }
                customer = customerBL.GetCustomerByUserNameAndPassWord(username, password);

                if (customer == null)
                {
                    Console.WriteLine("The account or password is incorrect");
                    choice = Utility.OnlyYN("Do you want to continue? Y/N: ");
                    switch (choice)
                    {
                        case "Y":
                            continue;
                        case "N":
                            Program();
                            break;
                        default:
                            continue;
                    }
                }
                else
                {
                    ConsoleCustomer cc = new ConsoleCustomer();
                    cc.MenuCustomer(customer);
                    break;
                }
            }
        }
        public bool ValidateLogin(string infoLogin)
        {
            Regex regex = new Regex("[a-zA-Z0-9_]");
            MatchCollection matchCollectionInfoLogin = regex.Matches(infoLogin);
            if (matchCollectionInfoLogin.Count < infoLogin.Length)
            {
                return false;
            }
            else if (infoLogin == " ")
            {
                return false;
            }
            else
            {
                return true;
            }


        }
        public string Password()
        {
            StringBuilder sb = new StringBuilder();
            while (true)
            {
                ConsoleKeyInfo cki = Console.ReadKey(true);
                if (cki.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    break;
                }
                if (cki.Key == ConsoleKey.Backspace)
                {
                    if (sb.Length > 0)
                    {
                        Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                        Console.Write(" ");
                        Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                        sb.Length--;
                    }
                    continue;
                }
                Console.Write('*');

                sb.Append(cki.KeyChar);
            }
            return sb.ToString();
        }
    }
}