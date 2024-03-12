using PieShopInventoryManagement.contracts;
using PieShopInventoryManagement.Domain.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PieShopInventoryManagement.Domain.ProductManagement
{
    public class RegularProduct : Product ,ISavable
    {
        public RegularProduct(int id, string name, string description, Price price, UnitType unitType, int maxItemsInStock) : base(id, name, description, price, unitType, maxItemsInStock)
        {
        }

        public string ConvertToStringForSaving()
        {
            return $"{ProductID};{ProductName};{ProductDescription};{maxItemsInStock};{Price.ItemPrice};{(int)Price.Currency};{(int)UnitType};4";
        }

        public override void IncreaseAmountInStock()
        {
            AmountInStock++;
        }
        public override object Clone()
        {
            return new RegularProduct(0, this.ProductName, this.productDescription, new Price() { ItemPrice = this.Price.ItemPrice, Currency = this.Price.Currency }, this.UnitType,this.maxItemsInStock);
        }
    }
}
