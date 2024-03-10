using PieShopInventoryManagement.Domain.General;
using PieShopInventoryManagement.Domain.ProductManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PieShopInventoryManagement
{
    internal class Program
    {
        static void Main(string[] args)
        {
          
            PrintWelcome();
            Console.WriteLine("press any key to continue!");
            Console.ReadLine();

            Utilities.InitializeStock();
            Utilities.ShowMainMenu();
            Console.WriteLine("Application shutting down...");
            Console.ReadLine();

        }
        static void PrintWelcome()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Welcome to Betheny's Pie Shop!");
            Console.ResetColor();
           

        }
    }
}
