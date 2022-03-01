using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Market_Management_System.Data.Entities;
using Market_Management_System.Data.Enums;

namespace Market_Management_System.Services
{
    public static class MenuServices
    {
        private static MarketService marketServices = new();

        #region Product Actions

        public static void AddProductMenu() //Add product menu method
        {
            Console.WriteLine("Enter the product name.");
            var name = Console.ReadLine();

            Console.WriteLine("Enter the price of the product.");
            var price = Console.ReadLine();

            Console.WriteLine("Enter the product number.");
            var quantity = Console.ReadLine();

            Console.WriteLine("Enter the product code.");
            var code = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter the product categoryş");

            //Select category from enum list
            ProductCategory category;

            Console.WriteLine("1. Bakery");
            Console.WriteLine("2. Dairy");
            Console.WriteLine("3. Meat");
            Console.WriteLine("4. Cleaners");
            Console.WriteLine("5. Beverages");
            Console.WriteLine("6. Produce");
            Console.WriteLine("7. Other");

            var input = Console.ReadLine();

            var sucess = Enum.TryParse<ProductCategory>(input, out category);

            if (!sucess)
            {
                Console.WriteLine("The {0} category you selected is incorrect.", input);
                return;
            }

            switch (category)
            {
                case ProductCategory.Bakery:
                    category = ProductCategory.Bakery;
                    break;
                case ProductCategory.Dairy:
                    category = ProductCategory.Dairy;
                    break;
                case ProductCategory.Meat:
                    category = ProductCategory.Meat;
                    break;
                case ProductCategory.Cleaners:
                    category = ProductCategory.Cleaners;
                    break;
                case ProductCategory.Beverages:
                    category = ProductCategory.Beverages;
                    break;
                case ProductCategory.Produce:
                    category = ProductCategory.Produce;
                    break;
                case ProductCategory.Other:
                    category = ProductCategory.Other;
                    break;

                default:
                    break;
            }

            try
            {
                marketServices.AddProduct(name, decimal.Parse(price).ToString(CultureInfo.InvariantCulture),
                    int.Parse(quantity), code,
                    category); //send product properties add product method as parameter
                Console.WriteLine("The product has been added");
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine("Price and quantity must be greater than 0");
                Console.WriteLine(e.Message);
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("The product name and code cannot be empty");
                Console.WriteLine(e.Message);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("There is already a product in this code");
                Console.WriteLine(e.Message);
            }
        }

        public static void UpdateProductMenu() //Edit product menu method 
        {
            Console.WriteLine("Enter the product code");
            var oldCode = Console.ReadLine();

            var products = marketServices.Products.Where(p => p.Code == oldCode).ToList();

            // Product(products);

            Console.WriteLine("Enter the product name");
            var newName = Console.ReadLine();

            Console.WriteLine("Enter the price of the product");
            var newPrice = Console.ReadLine();

            Console.WriteLine("Enter product quantity");
            var newQuantity = Console.ReadLine();

            Console.WriteLine("Enter the product quantity");
            var newCode = Console.ReadLine();

            Console.WriteLine("Enter the product category");

            //select category from enum list
            ProductCategory category;

            Console.WriteLine("1. Bakery");
            Console.WriteLine("2. Dairy");
            Console.WriteLine("3. Meat");
            Console.WriteLine("4. Cleaners");
            Console.WriteLine("5. Beverages");
            Console.WriteLine("6. Produce");
            Console.WriteLine("7. Other");

            var input = Console.ReadLine();

            var sucess = Enum.TryParse<ProductCategory>(input, out category);

            if (!sucess)
            {
                Console.WriteLine("The {0} category you selected is incorrect", input);
                return;
            }

            switch (category)
            {
                case ProductCategory.Bakery:
                    category = ProductCategory.Bakery;
                    break;
                case ProductCategory.Dairy:
                    category = ProductCategory.Dairy;
                    break;
                case ProductCategory.Meat:
                    category = ProductCategory.Meat;
                    break;
                case ProductCategory.Cleaners:
                    category = ProductCategory.Cleaners;
                    break;
                case ProductCategory.Beverages:
                    category = ProductCategory.Beverages;
                    break;
                case ProductCategory.Produce:
                    category = ProductCategory.Produce;
                    break;
                case ProductCategory.Other:
                    category = ProductCategory.Other;
                    break;

                default:
                    break;
            }

            try
            {
                marketServices.UpdateProduct(newCode, Convert.ToDecimal(newName), Convert.ToInt32(newPrice),
                    newQuantity, category, oldCode);
                Console.WriteLine("The product has been edited");
            }

            catch (Exception e)
            {
                Console.WriteLine("Please try again");
                Console.WriteLine(e.Message);
            }
        }

        public static void DeleteProductMenu() //Delete selected product menu method 
        {
            Console.WriteLine("Enter the code of the product you want to delete");
            var code = Console.ReadLine();

            var products = marketServices.Products.Where(p => p.Code == code).ToList();

            if (products.Count == 0) Console.WriteLine("The product you are looking for was not found");

            Console.WriteLine("Are you sure you want to delete the product? Y/N/Exit");
            var answer = Console.ReadLine()?.ToUpper();

            if (answer == "Y")
            {
                try
                {
                    marketServices.DeleteProduct(products.FirstOrDefault(p => p.Code == code)
                        ?.Code); //send product to delete method as parameter
                    Console.WriteLine("Deleted");
                }
                catch (ArgumentNullException e)
                {
                    Console.WriteLine("No code matching the number you typed.");
                    Console.WriteLine(e.Message);
                }
            }
            else if (answer == "N")
            {
                DeleteProductMenu();
            }
            else if (answer == "Exit")
            {
            }
        }

        #endregion

        #region Display Product

        public static void DisplayProductsByCategory() //Filter by category product list menu method 
        {
            Console.WriteLine("Enter the product category");

            //select category for filter
            ProductCategory category;

            Console.WriteLine("1. Bakery");
            Console.WriteLine("2. Dairy");
            Console.WriteLine("3. Meat");
            Console.WriteLine("4. Cleaners");
            Console.WriteLine("5. Beverages");
            Console.WriteLine("6. Produce");
            Console.WriteLine("7. Other");

            var input = Console.ReadLine();

            var sucess = Enum.TryParse<ProductCategory>(input, out category);

            if (!sucess)
            {
                Console.WriteLine("The {0} category you selected is incorrect", input);
                return;
            }

            switch (category)
            {
                case ProductCategory.Bakery:
                    category = ProductCategory.Bakery;
                    break;
                case ProductCategory.Dairy:
                    category = ProductCategory.Dairy;
                    break;
                case ProductCategory.Meat:
                    category = ProductCategory.Meat;
                    break;
                case ProductCategory.Cleaners:
                    category = ProductCategory.Cleaners;
                    break;
                case ProductCategory.Beverages:
                    category = ProductCategory.Beverages;
                    break;
                case ProductCategory.Produce:
                    category = ProductCategory.Produce;
                    break;
                case ProductCategory.Other:
                    category = ProductCategory.Other;
                    break;

                default:
                    break;
            }

            var products = marketServices.Products.FindAll(p => p.Category == category);
        }

        public static void DisplayProducts() //All products list menu method 
        {
            var marketServicesProducts = marketServices.Products;
        }

        public static void DisplayProductsByPriceRange() //Filter by price range product list menu method
        {
            Console.WriteLine("Please enter price range");

            Console.WriteLine("Minimum amount");
            var minPrice = double.Parse(Console.ReadLine());

            Console.WriteLine("Maximum amount");
            var maxPrice = double.Parse(Console.ReadLine());

            if (minPrice > maxPrice) Console.WriteLine("The amount range is incorrect");

            var sales =
                marketServices.Sales.Where(s => s.Amount >= (decimal) minPrice && s.Amount <= (decimal) maxPrice)
                    .ToList();

            if (sales.Count <= 0) Console.WriteLine("No search results found");
        }

        public static void DisplayProductsByName() // Filter by product name product list menu method 
        {
            Console.WriteLine("Enter the Product name");
            var name = Console.ReadLine();

            var products =
                marketServices.Products.FindAll(p => p.Name.ToLower().Contains(name.ToLower()));

            if (products.Count <= 0) Console.WriteLine("No search results found");
        }

        #endregion

        #region Display Sales

        public static void AddSaleProductMenu(Sale sale) //Add sale item menu method 
        {
            Console.WriteLine("Enter the product code");
            var code = Console.ReadLine();

            var product = marketServices.Products.FirstOrDefault(p => p.Code == code);

            if (product == null)
            {
                Console.WriteLine("The product code is incorrect");
                AddSaleProductMenu(sale);
            }

            Console.WriteLine("Enter quantity of the product");
            var quantity = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter price of the product");
            var price = double.Parse(Console.ReadLine());

            try
            {
                marketServices.AddSaleProduct(product, quantity, price, sale);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine("The quantity or price of the product is incorrect");
                throw;
            }
        }

        public static string AddOtherSaleProduct(Sale sale) //Add multiple sale item menu method 
        {
            Console.WriteLine("Want to add products? Y/N");
            var answer = Console.ReadLine()?.ToUpper();

            if (answer == "Y")
            {
                AddSaleProductMenu(sale);
                AddOtherSaleProduct(sale);
            }
            else if (answer == "N")
            {
                return answer;
            }
            else
            {
                Console.WriteLine("Your choice is not correct");
                AddOtherSaleProduct(sale);
            }

            return answer;
        }

        public static void AddSaleMenu() //Add  sale  menu method 
        {
            var sale = marketServices.AddSale(DateTime.Now); //Send date as parameter to add sale method

            AddSaleProductMenu(sale); //send sale to add sale item menu
            var answer = AddOtherSaleProduct(sale);

            Console.WriteLine("Sales added");

            DisplaySaleMenu();
        }

        public static void DeleteSingleSaleProductMenu() //Delete single sale item menu method 
        {
            Console.WriteLine("Enter the sales number");
            var no = Console.ReadLine();

            Console.WriteLine("Enter the product number");
            var saleItemNo = Console.ReadLine();

            try
            {
                marketServices.DeleteSingleSaleProduct(int.Parse(no),
                    saleItemNo); //send sale no and sale item no as parameter to delete selected sale item from sale  method
                Console.WriteLine("The product was successfully removed from sale");
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("No sales were found for the number you entered");
                DisplaySaleMenu();
            }
        }

        public static void DeleteSaleMenu() //Delete selected sale menu method 
        {
            Console.WriteLine("Enter the sale's number");
            var no = Console.ReadLine();

            try
            {
                marketServices.DeleteSale(int.Parse(no));
                Console.WriteLine("The sale was successfully deleted");
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("No sales were found for the number you entered");
                DisplaySaleMenu();
            }
        }

        public static void DisplaySales() // All sales list menu method 
        {
            var marketServicesSales = marketServices.Sales;
        }

        public static void DisplaySaleProducts() // All sales list menu method 
        {
            var marketServicesProductSales = marketServices.SaleProducts;
        }


        public static void DisplaySalesByDateRange() // Filter by date range sales list menu method 
        {
            Console.WriteLine("Enter the date range");

            Console.WriteLine("Start date (in MM.dd.yyyy format)");
            var minDate = DateTime.Parse(DateTime.Parse(Console.ReadLine()).ToString("MM.dd.yyyy"));

            Console.WriteLine("Expiration date (in MM.dd.yyyy format)");
            var maxDate = DateTime.Parse(DateTime.Parse(Console.ReadLine()).ToString("MM.dd.yyyy"));

            if (minDate > maxDate) Console.WriteLine("The date range is incorrect");

            var sales =
                marketServices.Sales.Where(s => s.Date >= minDate && s.Date <= maxDate).ToList();

            if (sales.Count <= 0)
            {
                Console.WriteLine("Axtarışa uyğun nəticə tapılmadı");
                DisplaySaleMenu();
            }
        }

        public static void DisplaySalesByPriceRange() // Filter by price range sales list menu method 
        {
            Console.WriteLine("Enter the price range");

            Console.WriteLine("Minimum amount");
            var minPrice = double.Parse(Console.ReadLine());

            Console.WriteLine("Maximum amount");
            var maxPrice = double.Parse(Console.ReadLine());

            if (minPrice > maxPrice) Console.WriteLine("The amount range is incorrect");

            var sales =
                marketServices.Sales.Where(s => s.Amount >= (decimal) minPrice && s.Amount <= (decimal) maxPrice)
                    .ToList();

            if (sales.Count <= 0)
            {
                Console.WriteLine("No search results found");
                DisplaySaleMenu();
            }
        }

        public static void DisplaySalesByDate() // Filter by date  sales list menu method 
        {
            Console.WriteLine("Enter the date (in MM.dd.yyyy format)");

            var date = DateTime.Parse(DateTime.Parse(Console.ReadLine()).ToString("MM.dd.yyyy"));

            var sales =
                marketServices.Sales.Where(s => s.Date.Date == date).ToList();

            if (sales.Count <= 0) Console.WriteLine("No search results found");
        }

        public static void DisplaySaleById() // Filter by saleNo  sale menu method 
        {
            Console.WriteLine("Enter the sales number");

            var no = int.Parse(Console.ReadLine());

            var sales =
                marketServices.Sales.Where(s => s.No == no).ToList();


            if (sales.Count == 0)
            {
                Console.WriteLine("No search results found");
                DisplaySaleMenu();
            }
        }

        #endregion

        public static void AddProdtest() // add default products for test 
        {
            marketServices.AddProd();
        }


        #region Display Menu

        public static void DisplayProductMenu()
        {
            var selection = 0;

            do
            {
                Console.WriteLine("1. Add new Product");
                Console.WriteLine("2. Update Product");
                Console.WriteLine("3. Delete Product");
                Console.WriteLine("4. All Products");
                Console.WriteLine("5. Products by category");
                Console.WriteLine("6. Products by Price Range");
                Console.WriteLine("7. Products by name");
                Console.WriteLine("8. Back to Homepage");
                Console.WriteLine("9.test");

                Console.WriteLine("Please choose");

                var selectionStr = Console.ReadLine();
                selection = int.Parse(selectionStr);


                switch (selection)
                {
                    case 1:
                        AddProductMenu();
                        break;
                    case 2:
                        UpdateProductMenu();
                        break;
                    case 3:
                        DeleteProductMenu();
                        break;
                    case 4:
                        DisplayProducts();
                        break;
                    case 5:
                        DisplayProductsByCategory();
                        break;
                    case 6:
                        DisplayProductsByPriceRange();
                        break;
                    case 7:
                        DisplayProductsByName();
                        break;
                    case 8:
                        Program.MainMenu();
                        break;
                    case 9:
                        AddProdtest();
                        break;
                    default:
                        break;
                }
            } while (selection != 8);
        } //Products sub menu

        public static void DisplaySaleMenu()
        {
            var selection = 0;

            do
            {
                Console.WriteLine("1. Yeni satış əlavə et");
                Console.WriteLine("2. Staışın anbara qayıtması");
                Console.WriteLine("3. Satışın silinməsi");
                Console.WriteLine("4. Satışların siyahısı");
                Console.WriteLine("5. Sales by Category");
                Console.WriteLine("6. Satış qiyməti aralığına görə satışlar");
                Console.WriteLine("7. Verilmiş tarixdəki satışlar");
                Console.WriteLine("8. Satış nömrəsinə görə axtarış");
                Console.WriteLine("9. Ana səhifəyə qayıt");

                Console.WriteLine("Please select your option");

                var selectionStr = Console.ReadLine();
                selection = int.Parse(selectionStr);


                switch (selection)
                {
                    case 1:
                        AddSaleMenu();
                        break;
                    case 2:
                        DeleteSingleSaleProductMenu();
                        break;
                    case 3:
                        DeleteSaleMenu();
                        break;
                    case 4:
                        DisplaySales();
                        break;
                    case 5:
                        DisplayProductsByCategory();
                        break;
                    case 6:
                        DisplaySalesByPriceRange();
                        break;
                    case 7:
                        DisplaySalesByDate();
                        break;
                    case 8:
                        DisplaySaleById();
                        break;
                    case 9:
                        Program.MainMenu();
                        break;
                    default:
                        break;
                }
            } while (selection != 9);
        }

        #endregion
    }
}