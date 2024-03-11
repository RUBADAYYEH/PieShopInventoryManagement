using PieShopInventoryManagement.Domain.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PieShopInventoryManagement.Domain.ProductManagement
{
    public class BulkProduct : Product
    {
        public BulkProduct(int id, string name, string description, Price price,  int maxItemsInStock) : base(id, name, description, price, UnitType.PerKg, maxItemsInStock)
        {
        }
    }
}
