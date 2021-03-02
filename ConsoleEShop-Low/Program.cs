using System;

namespace ConsoleEShop_Low
{
    class Program
    {
        static void Main(string[] args)
        {
            User user = null;
            string command;
            string[] arguments = new string[4];
            string output = "";
            Console.WriteLine("Type Help for list of commands");

            while (true)
            {
                Console.Write(">");
                command = Console.ReadLine();

                try
                {
                    if (user == null)
                    {
                        if (command == "Help")
                        {
                            output = $"Commands for unregistered user\n" +
                                    $"ShowProducts\n" +
                                    $"FindProduct\n" +
                                    $"RegisterUser\n" +
                                    $"LogIn\n";
                        }
                        else if (command == "LogIn")
                        {
                            Console.WriteLine("Login:");
                            arguments[0] = Console.ReadLine();
                            Console.WriteLine("Password:");
                            arguments[1] = Console.ReadLine();
                            User u = Shop.FindUser(arguments[0]);

                            if (u == null || !u.CheckPassword(arguments[1]))
                                output = $"Wrong login or password\n";
                            else
                            {
                                user = Shop.FindUser(arguments[0]);
                                output = $"Logged as {arguments[0]}\n";
                            }
                        }
                        else if (command == "LogOut")
                        {
                            output = "No active user to log out\n";
                        }
                        else if (command == "ShowProducts")
                        {
                            output = User.ShowProducts();
                        }
                        else if (command == "FindProduct")
                        {
                            Console.WriteLine("Product name:");
                            arguments[0] = Console.ReadLine();
                            output = User.FindProduct(arguments[0]);
                        }
                        else if (command == "RegisterUser")
                        {
                            Console.WriteLine("User name:");
                            arguments[0] = Console.ReadLine();
                            Console.WriteLine("Password:");
                            arguments[1] = Console.ReadLine();
                            Console.WriteLine("Adress:");
                            arguments[2] = Console.ReadLine();
                            user = Shop.RegisterUser(arguments[0], arguments[1], arguments[2]);

                            if (user != null)
                                output = $"User {arguments[0]} successfully registered\n";
                            else
                                output = $"User with name {arguments[0]} already exists\n";
                        }
                        else
                            output = "Unknown command try again\n";
                    }
                    else
                    {
                        if (command == "Help")
                        {
                            output = user.Commands();
                        }
                        else if (command == "LogIn")
                        {
                            output = "Already logged\n";
                        }
                        else if (command == "LogOut")
                        {
                            output = $"Logged out from {user.Name}\n";
                            user = null;
                        }
                        else if (command == "ShowProducts")
                        {
                            output = User.ShowProducts();
                        }
                        else if (command == "FindProduct")
                        {
                            Console.WriteLine("Product name:");
                            arguments[0] = Console.ReadLine();
                            output = User.FindProduct(arguments[0]);
                        }
                        else if (command == "CreateOrder")
                        {
                            Console.WriteLine("Product name:");
                            arguments[0] = Console.ReadLine();
                            Console.WriteLine("Product amount:");
                            arguments[1] = Console.ReadLine();
                            output = user.CreateOrder(arguments[0], Convert.ToInt32(arguments[1]));
                        }
                        else if (command == "CreateProduct")
                        {
                            if (user is Admin)
                            {
                                Console.WriteLine("Product name:");
                                arguments[0] = Console.ReadLine();
                                Console.WriteLine("Product category:");
                                arguments[1] = Console.ReadLine();
                                Console.WriteLine("Product description:");
                                arguments[2] = Console.ReadLine();
                                Console.WriteLine("Product price:");
                                arguments[3] = Console.ReadLine();
                                output = user.CreateProduct(arguments[0], arguments[1], arguments[2], Convert.ToDecimal(arguments[3]));
                            }
                            else
                                output = "Only admins can create products\n";
                        }
                        else if (command == "MarkOrder")
                        {
                            if (user is Admin)
                            {
                                Console.WriteLine("Order ID:");
                                arguments[0] = Console.ReadLine();
                                Console.WriteLine("Order new status:");
                                arguments[1] = Console.ReadLine();
                                output = user.ChangeOrder(Convert.ToInt32(arguments[0]), Shop.ToStatus(arguments[1]));
                            }
                            else
                                output = "Only admins can create products\n";
                        }
                        else if (command == "ShowOrders")
                        {
                            output = user.ShowOrders();
                        }
                        else if (command == "RecieveOrder")
                        {
                            Console.WriteLine("Order ID:");
                            arguments[0] = Console.ReadLine();
                            output = user.RecieveOrder(Convert.ToInt32(arguments[0]));
                        }
                        else if (command == "CancelOrder")
                        {
                            Console.WriteLine("Order ID:");
                            arguments[0] = Console.ReadLine();
                            output = user.CancelOrder(Convert.ToInt32(arguments[0]));
                        }
                        else if (command == "EditAdress")
                        {
                            if (user is Admin)
                            {
                                Console.WriteLine("User name:");
                                arguments[0] = Console.ReadLine();
                                Console.WriteLine("New adress:");
                                arguments[1] = Console.ReadLine();
                                output = user.ChangeUser(arguments[0], arguments[1]);
                            }
                            else
                            {
                                Console.WriteLine("New adress:");
                                arguments[0] = Console.ReadLine();
                                output = user.ChangeSelf(arguments[0]);
                            }
                        }
                        else if (command == "ChangePassword")
                        {
                            Console.WriteLine("Old password:");
                            arguments[0] = Console.ReadLine();
                            Console.WriteLine("New password:");
                            arguments[1] = Console.ReadLine();
                            output = user.ChangePassword(arguments[0], arguments[1]);
                        }
                        else
                            output = "Unknown command try again\n";
                    }
                }
                catch (Exception)
                {
                    output = "Something went wrong, try again\n";
                }

                Console.Write(output);
            }
        }
    }
}
