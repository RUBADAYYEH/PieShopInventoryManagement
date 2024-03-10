using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace PieShopInventoryManagement.Domain.OrderManagement
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime OrderFulfilmentDate {  get; set; }
        public List<OrderItem> OrderItems { get; }
        public bool Fulfilled { get; set; }=false;

        public Order()
        {
            OrderId = new Random().Next(9999999);

            int numberOfSeconds = new Random().Next(100); //FOR DEMO PURPOSES.
            OrderFulfilmentDate = DateTime.Now.AddSeconds(numberOfSeconds);
            OrderItems = new List<OrderItem>();
        }
        public string ShowOrderDetails()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Order ID: {OrderId}");
            sb.AppendLine($"Order Fullfillmennt date: {OrderFulfilmentDate.ToShortTimeString}");
            if (OrderItems != null)
            {
                foreach (var item in OrderItems)
                {
                        sb.AppendLine($"{item.ProductId} .{item.ProductName}:{item.AmountOrdered}");

                }
            }
            return sb.ToString();
        }
    }
}
