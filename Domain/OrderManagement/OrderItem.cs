﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PieShopInventoryManagement.Domain.OrderManagement
{
    public class OrderItem
    {
        public int OrderItemId { get; set; } 
        public int ProductId { get; set; }
        public string ProductName { get; set; }=string.Empty;
        public int AmountOrdered {  get; set; }

        public override string ToString()
        {
            return $"Product ID: {ProductId} - Name: {ProductName} - Amount ordered: {AmountOrdered}";
        }
    }
}
