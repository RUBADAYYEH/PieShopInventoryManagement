using PieShopInventoryManagement.Domain.General;
using PieShopInventoryManagement.Domain.OrderManagement;
using PieShopInventoryManagement.Domain.ProductManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace PieShopInventoryManagement
{
    internal class Utilities
    {
        private static List<Product> inventory = new();
        private static List<Order> orders = new();

        internal static void InitializeStock()
        {
           // BoxedProduct bp = new BoxedProduct(6,"Eggs","bla",new Price() { Currency=Currency.Euro,ItemPrice=10},100,6);
            
           ProductRepository productRepository = new ProductRepository();
            inventory = productRepository.LoadProductsFromFile();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Loaded {inventory.Count} products!");
            Console.WriteLine("Press enter to countinue!");
            Console.ResetColor();
            Console.ReadLine();
        }
        internal static void ShowMainMenu()
        {
            Console.ResetColor();
            Console.Clear();
            Console.WriteLine("********************");
            Console.WriteLine("* Select an Action *");
            Console.WriteLine("********************");
            Console.WriteLine("1: Inventory management");
            Console.WriteLine("2: Order management");
            Console.WriteLine("3: Settings");
            Console.WriteLine("4: Save all data");
            Console.WriteLine("0: Close application");
            Console.WriteLine("Your selection: ");
            string? userSelection=Console.ReadLine();
            switch (userSelection)
            {
                case "1":
                    ShowInventoryManagementMenu();
                    break;
                case "2":
                    ShowOrderManagementMenu();
                    break;
                case "3":
                    ChangeStockThreshold();
                    break;
                case "4":
                    break;
                case "0":
                    break;
                default:
                    Console.WriteLine("Invalid selection. Please try again.");
                    break;


            }

            
        }

        private static void ShowOrderManagementMenu()
        {
            string? userSelection = string.Empty;
            do
            {
                Console.ResetColor();
                Console.Clear();
                Console.WriteLine("********************");
                Console.WriteLine("* Select an Action *");
                Console.WriteLine("********************");
                Console.WriteLine("1: Open Order overview");
                Console.WriteLine("2: Add new Order");
                Console.WriteLine("0: Back to main menu");

                Console.WriteLine("Your selection: ");
                userSelection = Console.ReadLine();
                switch (userSelection)
                {
                    case "1":
                        ShowOpenOrderOverview();
                        break;
                    case "2":
                        ShowAddNewOrder();
                        break;
                    default:
                        Console.WriteLine("Invalid selection. Please try again.");
                        break;
                }
            } while (userSelection != "0");
            Console.Clear();
            ShowMainMenu();
        }

        private static void ShowAddNewOrder()
        {
            Order neworder=new Order();
            string? selectedProductId=string.Empty;

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Creating new Order");
            Console.ResetColor();

            do
            {
                ShowAllProductsOverview();

                Console.WriteLine("Which product do you want to order? (enter 0 to stop adding new products to the order");
                Console.WriteLine("Enter the ID of product: ");
                selectedProductId = Console.ReadLine();
                if (selectedProductId != "0")
                {
                    Product? selectedProduct = inventory.Where(p => p.ProductID == int.Parse(selectedProductId)).FirstOrDefault();
                    if (selectedProduct != null)
                    {
                        Console.WriteLine("How many do oyu want to order?");
                        int amountOrdered = int.Parse(Console.ReadLine());
                        OrderItem orderItem = new OrderItem
                        {
                            ProductId = selectedProduct.ProductID,
                            ProductName = selectedProduct.ProductName,
                            AmountOrdered = amountOrdered
                        };
                        neworder.OrderItems.Add(orderItem);
                    }
                }
            } while (selectedProductId != "0");
            
        }
        private static void ChangeStockThreshold()
        {
            Console.WriteLine($"Enter new stock threshold (current threshold value : {Product.stockThreshold}) this applies to all products!");
            Console.Write("New value: ");
            int newValue=int.Parse(Console.ReadLine()??"0");
            Product.stockThreshold = newValue;
            Console.WriteLine($"New stock threshold value set to {Product.stockThreshold}");
            foreach(var item in inventory)
            {
                item.UpdateLowStock();
            }        
        }

        private static void ShowOpenOrderOverview()
        {
            //check to handle fullfilled orders
            ShowFulfilledOrders();
            if (orders.Count > 0)
            {
                Console.WriteLine("Open Orders:");
                foreach (var order in orders)
                {

                    Console.WriteLine(order.ShowOrderDetails());
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("There are no open orders");
            }
        }
        private static void ShowFulfilledOrders()
        {
            Console.WriteLine("Checking fulfilled orders:");
            foreach (var order in orders)
            {
                if (order.Fulfilled && order.OrderFulfilmentDate < DateTime.Now)
                {
                    foreach(var orderItem in order.OrderItems)
                    {
                        Product? selectedProduct=inventory.Where(p => p.ProductID == orderItem.ProductId).FirstOrDefault();
                        if (selectedProduct != null)
                        {
                            selectedProduct.IncreaseAmountInStock(orderItem.AmountOrdered);
                        }
                        
                    }
                    order.Fulfilled = true;
                }

         
            }
            orders.RemoveAll(o => o.Fulfilled);
            Console.WriteLine("Filfilled orders checked");
        }

        private static void ShowInventoryManagementMenu()
        {
            string? userSelection;
            do
            {
                Console.ResetColor();
                Console.Clear();
                Console.WriteLine("************************");
                Console.WriteLine("* Inventory Management *");
                Console.WriteLine("************************");
                
                ShowAllProductsOverview();

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("What do you want to do?");
                Console.ResetColor();
                
                Console.WriteLine("1: View details of product");
                Console.WriteLine("2: Add new product");
                Console.WriteLine("3: Clone product");
                Console.WriteLine("4: View products with low stock");
                Console.WriteLine("0: Back to main menu");
                Console.WriteLine("Your selection: ");
               
                userSelection = Console.ReadLine();

                switch (userSelection)
                {
                    case "1":
                        ShowDetailsAndUseProduct();
                        break;
                    case "2":
                        ShowCreateNewProduct();
                        break;
                    case "3":

                        break;
                    case "4":
                        ShowProductsLowOnStock();
                        break;
                    case "0":
                        break;
                    default:
                        Console.WriteLine("Invalid selection. Please try again.");
                        break;


                }

            } while (userSelection != "0");
            Console.Clear();
            ShowMainMenu();
        }
        private static void ShowAllUnitTypes()
        {
            int i = 1;
            foreach (string name in Enum.GetNames(typeof(UnitType)))
            {
              Console.WriteLine($"{i}.{name}");
                i++;
            }
        }
        private static void ShowAllCurrencies()
        {
            int i = 1;
            foreach (string name in Enum.GetNames(typeof(Currency)))
            {
                Console.WriteLine($"{i}.{name}");
                i++;
            }
        }

        private static void ShowCreateNewProduct()
        {
            Console.WriteLine("What type of product od you want to crete?");
            Console.WriteLine("1. Regular product\n2. Bulk product\n3. Fresh product\n4. Boxed product");
            Console.WriteLine("Your selection:");
            var productType=Console.ReadLine();
            if (productType!="1" && productType!="2" && productType!="3" && productType != "4")
            {
                Console.WriteLine("Invalid Selection!");
                return;
            }
            Product? newProduct = null;
            Console.WriteLine("Enter the name of the product: ");
            string name = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("Enter the price of the product: ");
            double price = double.Parse(Console.ReadLine() ?? "0.0");
            ShowAllCurrencies();
            Console.WriteLine("Select a currency: ");
            Currency currency = (Currency)Enum.Parse(typeof(Currency), Console.ReadLine() ?? "1");
            Console.WriteLine("Enter the description of the product: ");
            string description = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("Enter the maximum items in stock for the product: ");
            int max = int.Parse(Console.ReadLine() ?? "0");
            int newID = inventory.Max(p => p.ProductID) + 1;

            switch (productType)
            {
                case "1":
                    newProduct = new RegularProduct(newID, name, description, new Price() { Currency = currency, ItemPrice = price }, UnitType.PerItem,max);
                    break;
                case "2":
                    newProduct = new BulkProduct(newID++, name, description, new Price() { Currency = currency, ItemPrice = price }, max);
                    break;
                case "3":
                    newProduct = new FreshProduct(newID++, name, description, new Price() { Currency = currency, ItemPrice = price },UnitType.PerItem, max);
                    break;
                case "4":
                    Console.WriteLine("Enter the number of items in box for the product: ");
                    int numberInBox = int.Parse(Console.ReadLine() ?? "0");
                    newProduct = new BoxedProduct(newID++, name, description, new Price() { Currency = currency, ItemPrice = price }, max,numberInBox);
                    break;


            }
            if (newProduct != null)
            {
                inventory.Add(newProduct);
            }
        }

        private static void ShowProductsLowOnStock()
        {
            List<Product> lowOnStockproducts = inventory.Where(p => p.isBelowStockThreshold).ToList();
            if (lowOnStockproducts.Count > 0)
            {
                Console.WriteLine("The following items are low on stock. order more soon!.");
                foreach (var product in lowOnStockproducts)
                {
                    Console.WriteLine(product.DisplayDetailsShort());
                    Console.WriteLine();
                }

            }
            else
            {
                Console.WriteLine("No items are currently low on stock");
            }
        }

        private static void ShowAllProductsOverview()
        {
            foreach(var product in inventory)
            {
                Console.WriteLine(product.DisplayDetailsShort());
                Console.WriteLine();
            }
        }
        private static void ShowDetailsAndUseProduct()
        {
            string? userSelection = String.Empty;
            Console.WriteLine("Enter the ID of the product: ");
            string userProductId = Console.ReadLine();

            if (userProductId != null)
            {
                Product? selectedProduct=inventory.Where(p => p.ProductID==int.Parse(userProductId)).FirstOrDefault();
                if (selectedProduct != null)
                {
                    Console.WriteLine(selectedProduct.DisplayDetailsFull());

                    Console.WriteLine("\nWhat do you want to do?");
                    Console.WriteLine("1: Use product");
                    Console.WriteLine("0: Back to inventory overview");
                    Console.WriteLine("Your selection: ");
                    userSelection= Console.ReadLine();
                  if (userSelection == "1")
                    {
                        Console.WriteLine("How many products do you want to use?");
                        int amount =int.Parse(Console.ReadLine()?? "0");
                        selectedProduct.UseProduct(amount);
                        Console.ReadLine();
                    }
                }
            }
        }

    }
}
