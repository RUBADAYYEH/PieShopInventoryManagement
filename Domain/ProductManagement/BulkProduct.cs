using PieShopInventoryManagement.contracts;
using PieShopInventoryManagement.Domain.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PieShopInventoryManagement.Domain.ProductManagement
{
    public class BulkProduct : Product,ISavable
    {
        public BulkProduct(int id, string name, string description, Price price,  int maxItemsInStock) : base(id, name, description, price, UnitType.PerKg, maxItemsInStock)
        {
        }

        public string ConvertToStringForSaving()
        {
            return $"{ProductID};{ProductName};{ProductDescription};{maxItemsInStock};{Price.ItemPrice};{(int)Price.Currency};{(int)UnitType};3;";
        }

        public override void IncreaseAmountInStock()
        {
         AmountInStock++;
        }
        public override object Clone()
        {
            return new BulkProduct(0, this.ProductName, this.productDescription, new Price() { ItemPrice = this.Price.ItemPrice, Currency = this.Price.Currency }, this.maxItemsInStock);
        }
    }
}
