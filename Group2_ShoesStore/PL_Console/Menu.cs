using System;
using Persistence;
using BL;
using System.Text;
using System.Text.RegularExpressions;
using ConsoleTables;
using System.Net.Mail;
namespace PL_Console
{
    public class Menu
    {
        public void Program()
        {
            while (true)
            {

                string[] choice = { "Login", "Sign Up", "Exit" };
                short choose = Utility.MenuTemplate("Shoes Stores", choice);
                switch (choose)
                {
                    case 1:
                        MenuLogin();
                        continue;
                    case 2:
                        MenuSignUp();
                        continue;
                    case 3:
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
        public void MenuSignUp()
        {
            CustomerBL customerBL = new CustomerBL();
            string Username = null;
            string password = null;
            string Name = null;
            string Email = null;
            string Phone = null;
            string Birthday = null;
            string Gender = null;
            string Address = null;
            string choice2;
            string choice3;
            while (true)
            {
                Console.Clear();
                string row1 = "=====================================================================";
                string row2 = "---------------------------------------------------------------------";
                Console.WriteLine(row1);
                Console.WriteLine("Enter Information");
                Console.WriteLine(row2);
                Console.Write("Username: ");
                Username = Console.ReadLine();
                Console.Write("Password: ");
                password = Password();
                Console.Write("Email: ");
                Email= Console.ReadLine();
                // Email = EmailVali();
                Console.Write("Full Name: ");
                Name = Console.ReadLine();
                Console.Write("Phone Number: ");
                Phone = Console.ReadLine();
                Console.Write("Date Of Birth: ");
                Birthday = Console.ReadLine();
                Console.Write("Gender: ");
                Gender = Console.ReadLine();
                Console.Write("Address: ");
                Address = Console.ReadLine();
                int flag = customerBL.VerifyRegister(Username, Email);
                if (flag == 2)
                {
                    customerBL.Register(Username, password, Name, Email, Phone, Birthday, Gender, Address);
                    // Console.WriteLine("Registration successful");
                    choice2 = Utility.OnlyYN("Do you want to continue? Y/N: ");
                    switch (choice2)
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
                    choice3 = Utility.OnlyYN("Do you want to continue? Y/N: ");
                    switch (choice3)
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
        // public string EmailVali()
        // {
        //     string Email = Console.ReadLine();
        //     while (true)
        //     {
        //         if (Email.Contains("@"))
        //     {
        //         Console.WriteLine(" Valid Email! ");
                
        //     }
        //    else
        //    {
        //        Console.WriteLine("Invalid Email, Plz enter again :)");
        //        break;
               
        //    }
        //     }
        //     return Email;
        
           

        // }
        
    }
}