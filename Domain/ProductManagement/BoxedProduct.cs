using PieShopInventoryManagement.Domain.General;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace PieShopInventoryManagement.Domain.ProductManagement
{
    public class BoxedProduct : Product
    {
        private int amountPerBox;
        public int AmountPerBox
        {
            get { return amountPerBox; }
            set
            { amountPerBox = value; }
        }
        public BoxedProduct(int id, string name, string? description, Price price,  int maxItemsInStock,int amountPerBox) : base(id, name, description, price, UnitType.PerBox, maxItemsInStock)
        {
            AmountPerBox = amountPerBox;
        }
        public override string DisplayDetailsFull()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Boxed Product\n");
            sb.Append($"{ProductID} {ProductName} \n {ProductDescription}\n{Price}\n{AmountInStock} item(s) in stock");
            if (isBelowStockThreshold)
            {
                sb.Append("\n!!STOCK LOW!!");
            }       
            return sb.ToString();
        }
        public override void UseProduct(int items) {
            int smallestMultiple = 0;
            int batchSize;
            while (true)
            {
                smallestMultiple++;
                if (smallestMultiple * AmountPerBox > items)
                {
                    batchSize = (smallestMultiple * AmountPerBox);
                    break;
                }
            }
            base.UseProduct(batchSize); //using base method

        }
        public override void IncreaseAmountInStock()
        {
            AmountInStock += amountPerBox;
        }
        public override void IncreaseAmountInStock(int items)
        {
            int newStock = AmountInStock + items * AmountPerBox;
            if (newStock <= maxItemsInStock) 
            {
                AmountInStock += items * AmountPerBox;
            }
            else
            {
                AmountInStock = maxItemsInStock;
                Log($"{CreateSimpleProductRepresentation} stock overflow. {newStock-AmountInStock} items that couldn't be stored"); 
            }
            if (AmountInStock >= stockThreshold)
            {
                isBelowStockThreshold = false;
            }
            
        }
        /* public void UseBoxedProduct(int items)
         {
             int smallestMultiple = 0;
             int batchSize;
             while (true)
             {
                 smallestMultiple++;
                 if (smallestMultiple * AmountPerBox > items)
                 {
                     batchSize = (smallestMultiple * AmountPerBox);
                     break;
                 }
             }
             UseProduct(batchSize); //using base method

         }*/

    }
}
