using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PieShopInventoryManagement.Domain.ProductManagement
{
    public partial class Product
    {
        private void UpdateLowStock()
        {
            if (AmountInStock < 10) //it's a fixed value for now for (maxItemsInStock)
            {
                isBelowStockThreshold = true;
            }
            if (AmountInStock >= 10)
            {
                isBelowStockThreshold = false;
            }
        }
        private void Log(string message)
        {
            Console.WriteLine(message);
        }

        private string CreateSimpleProductRepresentation()
        {
            return $"Product{productID} ({productName})";
        }
    }
}
