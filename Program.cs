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
            //CREATING OBJECT OD PRODUCT.
            Price samplePrice = new Price() {ItemPrice= 10, Currency=Currency.Euro };
            Product p1 = new Product(1, "Sugar", "Lorem ipsum", samplePrice, UnitType.PerKg, 100);

            p1.IncreaseAmountInStock(10);
            p1.ProductDescription = "Sample description";


            //USING IMPLICIT TYPING 
            var p2 = new Product(2, "Cake decorations", "Lorem ipsum", samplePrice, UnitType.PerItem, 100);

            Product p3 = new(3, "Strawberry", "Lorem ipsum", samplePrice, UnitType.PerBox, 10);

            //USING OBJECT INITIALIZAION
            Product p4 = new Product() { ProductName = "Eggs", ProductID = 123 };
            
            
            
            PrintWelcome();

        }
        static void PrintWelcome()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Welcome to Betheny's Pie Shop!");
            Console.ResetColor();
        }
    }
}
