using PieShopInventoryManagement.contracts;
using PieShopInventoryManagement.Domain.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PieShopInventoryManagement.Domain.ProductManagement
{
    public class FreshProduct : Product,ISavable
    {
        public DateTime ExpiaryDateTime { get; set; }
        public string? StorageInstructions { get; set; }
        public FreshProduct(int id, string name, string description, Price price, UnitType unitType, int maxItemsInStock) : base(id, name, description, price, unitType, maxItemsInStock)
        {
        }
        public override string DisplayDetailsFull()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{productID} {productName} \n{productDescription} \n {Price}\n{AmountInStock} items in stock.");
            if (isBelowStockThreshold)
            {
                sb.Append("\n !!STOCK LOW!!");
            }
            sb.AppendLine("Storage instructions: " + StorageInstructions);
            sb.AppendLine("Expiry date: " + ExpiaryDateTime.ToShortDateString);
            return sb.ToString();        
        }
        public override void IncreaseAmountInStock()
        {
            AmountInStock++;
        }

        public string ConvertToStringForSaving()
        {
            return $"{ProductID};{ProductName};{ProductDescription};{maxItemsInStock};{Price.ItemPrice};{(int)Price.Currency};{(int)UnitType};2;";
        }
        public override object Clone()
        {
            return new FreshProduct(0, this.ProductName, this.productDescription, new Price() { ItemPrice = this.Price.ItemPrice, Currency = this.Price.Currency },this.UnitType, this.maxItemsInStock);
        }
    }
}
