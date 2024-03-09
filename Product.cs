﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PieShopInventoryManagement
{
    public class Product
    {
        private int productID;
        private string productName = String.Empty;
        private string? productDescription;

        private int maxItemsInStock = 0;

        

        //PROPERTIES 
        public int ProductID
        {
            get { return productID; }
            set { productID = value; }
        }
        public string ProductName
        {
            get { return productName; }
            set {
                productName = (value.Length > 50) ? value[..50] : value;
            }
        }
        public string ProductDescription
        {
            get { return productDescription; }
            set {
                if (value == null)
                {
                    productDescription = String.Empty;
                }
                else
                {
                    productDescription = value.Length > 250 ? value[..250] : value;
                }
            }
        }

        //ONLY ACCESSED AS PROPERTIES
        public UnitType UnitType { get; set; }
        public int AmountInStock { get; private set; }
        public bool isBelowStockThreshold { get; private set; }

        public Product(int id,string name) { //using properties
            ProductID = id;        
            ProductName = name;
        }
        public Product(int id):this(id, String.Empty){ }
        
        public Product()
        {
            maxItemsInStock = 100;
            ProductName = String.Empty;
        }
        public Product(int id, string name,string? description,UnitType unitType,int maxItemsInStock)
        { 
            ProductID = id;
            ProductName = name;
            ProductDescription = description;
            UnitType = unitType;
            this.maxItemsInStock = maxItemsInStock;

            UpdateLowStock();
        }




        public void IncreaseAmountInStock()
        {
            AmountInStock++;
        }
        public void IncreaseAmountInStock(int amount)
        {
           
            if ((AmountInStock+amount) <= maxItemsInStock)
            {
                AmountInStock += amount;
            }
            else
            {
                AmountInStock = maxItemsInStock; // we only store the possible items overstock isn't stored
                Log($"{CreateSimpleProductRepresentation} stock overflow. {(AmountInStock + amount) - AmountInStock} item(s) ordered that couldn't be stored");
            }
            UpdateLowStock() ;
        }
        private void DecreaseAmountInStock(int items,String reason) // accesses in a class level .
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

        public void UseProduct(int items)  //this will allow to keep track of the amount in stock whenver items are used and decremented.
        {
            if (items<= AmountInStock)
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
        private void Log(String message)
        {
            Console.WriteLine(message);
        }

        private string CreateSimpleProductRepresentation()
        {
            return $"Product{productID} ({productName})";
        }
        public string DisplayDetailsFull()  //this will provide and ALERT IF STOCK LOW!
        {
            StringBuilder sb =new StringBuilder();
            sb.Append($"{productID} {productName} \n{productDescription} \n{AmountInStock} items in stock.");
            if (isBelowStockThreshold)
            {
                sb.Append("\n !!STOCK LOW!!");
            }
            return sb.ToString();
        }

        public string DisplayDetailsShort()
        {
            return $"{productID}.{productName} \n{AmountInStock} items in stock.";
        }



    }
}
