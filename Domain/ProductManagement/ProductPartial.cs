using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PieShopInventoryManagement.Domain.ProductManagement
{
    public partial class Product
    {
        public void UpdateLowStock()
        {
            if (AmountInStock < stockThreshold) //it's a fixed value for now for (maxItemsInStock)
            {
                isBelowStockThreshold = true;
            }
            if (AmountInStock >= stockThreshold)
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
