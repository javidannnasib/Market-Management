using System;
using Market_Management_System.Services;

namespace Market_Management_System
{
    class Program
    {
        static void Main(string[] args)
        {
            MainMenu();
        }


        public static void MainMenu()
        {
            
            int selection = 0;
            
            do
            {
                Console.WriteLine("1. Products");
                Console.WriteLine("2. Sales");
                Console.WriteLine("3. Exit");

                Console.WriteLine("Please select your option");

                string selectionStr = Console.ReadLine();
                selection = int.Parse(selectionStr);


                switch (selection)
                {
                    case 1:
                        MenuServices.DisplayProductMenu();
                        break;
                    case 2:
                        MenuServices.DisplaySaleMenu();
                        break;
                    case 3:
                        Console.WriteLine("Good bye");
                        break;
                    default:
                        break;
                }

            } while (selection != 3);
        }
    }
}