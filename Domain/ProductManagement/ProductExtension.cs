using PieShopInventoryManagement.Domain.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PieShopInventoryManagement.Domain.ProductManagement
{
    internal static class ProductExtension
    {
        static double dollarToEuro = 0.92;
        static double euroToDollar = 1.11;

        static double poundToEuro = 1.14;
        static double eurotoPound = 0.88;

        static double dollarToPound = 0.81;
        static double poundToDollar = 1.14;

        public static double ConvertProductPrice(this Product product,Currency targetCurrency)
        {
            Currency currency = product.Price.Currency;
            double productPrice = product.Price.ItemPrice;
            double convertedPrice = 0.0;
            if (currency==Currency.Dollar && targetCurrency == Currency.Euro)
            {
                convertedPrice = productPrice * dollarToEuro;
            }
            else if (currency == Currency.Euro && targetCurrency == Currency.Dollar)
            {
                convertedPrice = productPrice * euroToDollar;
            }

            else if (currency == Currency.Pound && targetCurrency == Currency.Euro)
            {
                convertedPrice = productPrice * poundToEuro;
            }
            else if (currency == Currency.Euro && targetCurrency == Currency.Pound)
            {
                convertedPrice = productPrice * eurotoPound;
            }
            else if (currency == Currency.Dollar && targetCurrency == Currency.Pound)
            {
                convertedPrice = productPrice * dollarToPound;
            }
            else if (currency == Currency.Pound && targetCurrency == Currency.Dollar)
            {
                convertedPrice = productPrice * dollarToPound;
            }
            return convertedPrice;
        }
    }
}
