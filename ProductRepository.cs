using PieShopInventoryManagement.Domain.General;
using PieShopInventoryManagement.Domain.ProductManagement;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PieShopInventoryManagement
{
    internal class ProductRepository
    {
        private string direcotry = @"C:\Users\Lenovo\source\repos\PieShopInventoryManagement\data\";
        private string productsFileName = "products.txt";

        private  void CheckForExistingProductFile()
        {
            string path = $"{direcotry}{productsFileName}";
            bool existingFileFound =File.Exists(path);
            if (!existingFileFound) {
            //Create the directory
            if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
            //Create the file
            using FileStream fs=File.Create(path);
            
            }
        }
        public List<Product> LoadProductsFromFile()
        {
            List<Product> products = new List<Product>();
            string path = $"{direcotry}{productsFileName}";
            try
            {
                CheckForExistingProductFile();

                string[] productsAsString = File.ReadAllLines(path);
                for(int i = 0; i < productsAsString.Length; i++)
                {
                    string[] productsSplits = productsAsString[i].Split(';');
                    bool success = int.TryParse(productsSplits[0], out int productId);
                    if (!success)
                    {
                        productId = 0;
                    }
                    string name = productsSplits[1];
                    string desc = productsSplits[2];
                    success = int.TryParse(productsSplits[3], out int maxItemsInStock);
                    if (!success)
                    {
                        maxItemsInStock = 100; //default value
                    }
                    success = int.TryParse(productsSplits[4], out int itemPrice);
                    if (!success)
                    {
                        itemPrice = 0;
                    }
                    success =
                        Enum.TryParse(productsSplits[5], out Currency currency);
                    if (!success)
                    {
                        currency = Currency.Dollar;
                    }
                    success = Enum.TryParse(productsSplits[6], out UnitType unitType);
                    if (!success)
                    {
                        unitType = UnitType.PerItem;
                    }
                    string productType = productsSplits[7];
                    Product product = null;
                    switch (productType)
                    {
                        case "1":
                            success = int.TryParse(productsSplits[8], out int amountPerBox);
                            if (!success)
                            {
                                amountPerBox = 1; //default
                            }
                            product = new BoxedProduct(productId, name, desc, new Price() { ItemPrice = itemPrice, Currency = currency }, maxItemsInStock, amountPerBox);
                            break;
                        case "2":

                            product = new FreshProduct(productId, name, desc, new Price() { ItemPrice = itemPrice, Currency = currency }, unitType, maxItemsInStock);
                            break;
                        case "3":

                            product = new BulkProduct(productId, name, desc, new Price() { ItemPrice = itemPrice, Currency = currency }, maxItemsInStock);
                            break;
                        case "4":

                            product = product = new RegularProduct(productId, name, desc, new Price() { ItemPrice = itemPrice, Currency = currency }, unitType, maxItemsInStock);
                            break;
                    }

                        
                   
                    products.Add(product);
                }
            }
            catch(IndexOutOfRangeException iex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Something went wrong when parsing the file,please check the data!");
                Console.WriteLine(iex.Message);
            }
            catch(FileNotFoundException fnex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The file couldn't be found!");
                Console.WriteLine(fnex.Message);
                Console.WriteLine(fnex.StackTrace);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Something went wrong while loading the file!");
                Console.WriteLine(ex.Message);
                
            }
            finally
            {
                Console.ResetColor();
            }
            return products;
        }
    }

}
