﻿using PieShopInventoryManagement.Domain.General;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PieShopInventoryManagement.Domain.ProductManagement
{
    public abstract partial class Product : ICloneable
    {
        public static int stockThreshold = 5;

        protected int productID;
        protected string productName = string.Empty;
        protected string productDescription;

        protected int maxItemsInStock = 0;



        //PROPERTIES 
        public int ProductID
        {
            get { return productID; }
            set { productID = value; }
        }
        public string ProductName
        {
            get { return productName; }
            set
            {
                productName = value.Length > 50 ? value[..50] : value;
            }
        }
        public string ProductDescription
        {
            get { return productDescription; }
            set
            {
                if (value == null)
                {
                    productDescription = string.Empty;
                }
                else
                {
                    productDescription = value.Length > 250 ? value[..250] : value;
                }
            }
        }

        //ONLY ACCESSED AS PROPERTIES
        public UnitType UnitType { get; set; }
        public int AmountInStock { get; protected set; }
        public bool isBelowStockThreshold { get; protected set; }
        public Price Price { get;  set; }
        public Product(int id, string name)
        { //using properties
            ProductID = id;
            ProductName = name;
        }
        public Product(int id) : this(id, string.Empty) { }

        public Product()
        {
            maxItemsInStock = 100;
            ProductName = string.Empty;
        }
        public  Product(int id, string name, string? description,Price price, UnitType unitType, int maxItemsInStock)
        {
            ProductID = id;
            ProductName = name;
            ProductDescription = description;
            Price = price;
            UnitType = unitType;
            this.maxItemsInStock = maxItemsInStock;
            

            UpdateLowStock();
        }
        public  static void ChangeStickThreshold(int newStockThreshold)
        {
            stockThreshold = newStockThreshold;  
        }




        // public virtual void IncreaseAmountInStock()
        //{
        //  AmountInStock++;
        //}
        public abstract void IncreaseAmountInStock();
        public virtual void IncreaseAmountInStock(int amount)
        {

            if (AmountInStock + amount <= maxItemsInStock)
            {
                AmountInStock += amount;
            }
            else
            {
                AmountInStock = maxItemsInStock; // we only store the possible items overstock isn't stored
                Log($"{CreateSimpleProductRepresentation} stock overflow. {AmountInStock + amount - AmountInStock} item(s) ordered that couldn't be stored");
            }
            UpdateLowStock();
        }
        protected virtual void DecreaseAmountInStock(int items, string reason) // accesses in a class level .
        {
            if (items <= AmountInStock)
            {
                AmountInStock -= items;
            }
            else
            {
                AmountInStock = 0;
            }
            UpdateLowStock(); //THIS WILL SEND NOTIFICATION IF STOCK LOW
            Log(reason);
        }

        public virtual void UseProduct(int items)  //this will allow to keep track of the amount in stock whenver items are used and decremented.
        {
            if (items <= AmountInStock)
            {
                AmountInStock -= items;
                UpdateLowStock();
                Log($"Amount in stock updated. Now {AmountInStock} items in stock.");
            }
            else
            {
                Log($"Not enough items in stock for {CreateSimpleProductRepresentation()}. {AmountInStock} available but {items} requested.");
            }
        }
       
        public virtual string DisplayDetailsFull()  //this will provide and ALERT IF STOCK LOW!
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{productID} {productName} \n{productDescription} \n {Price}\n{AmountInStock} items in stock.");
            if (isBelowStockThreshold)
            {
                sb.Append("\n !!STOCK LOW!!");
            }
            return sb.ToString();
        }

        public virtual string DisplayDetailsShort()
        {
            return $"{productID}.{productName} \n{AmountInStock} items in stock.";
        }
        public abstract object Clone();

        /*public object Clone()
        {
            throw new NotImplementedException();
        }*/
    }
}
