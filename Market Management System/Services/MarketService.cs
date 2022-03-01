using System;
using System.Collections.Generic;
using System.Linq;
using Market_Management_System.Data.Entities;
using Market_Management_System.Data.Enums;

namespace Market_Management_System.Services
{
    public class MarketService
    {
        public MarketService()
        {
            SaleProducts = new List<SaleProduct>();
            Sales = new List<Sale>();
            Products = new List<Product>();
        }

        public List<Product> Products { get; private set; }
        public List<Sale> Sales { get; private set; }
        public List<SaleProduct> SaleProducts { get; private set; }

        #region Product
        //Adding Product method
        public int AddProduct(string name, string code, decimal price, int quantity, ProductCategory category)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException("name");
            if (string.IsNullOrEmpty(code)) throw new ArgumentNullException("code");
            if (quantity <= 0) throw new ArgumentOutOfRangeException("quantity");
            if (price <= 0) throw new ArgumentOutOfRangeException("price");

            var product = new Product();
            product.Code = code;
            product.Price = price;
            product.Quantity = quantity;
            product.Name = name;
            product.Category = category;

            Products.Add(product);

            return product.No;
        }
        
        public void AddProd() //Add default products for test 
        {
            Product product = new Product();

            product.Name = "test1";
            product.Price = 5;
            product.Quantity = 20;
            product.Code = "123";
            product.Category = ProductCategory.Cleaners;
            
            Products.Add(product);
            
            Product product1 = new Product();

            product1.Name = "test2";
            product1.Price = 3;
            product1.Quantity = 30;
            product1.Code = "321";
            product1.Category = ProductCategory.Produce;
            
            Products.Add(product1);
        } 
        
        //Update Product method
        public int UpdateProduct(string newName, decimal newPrice, int newQuantity, string newCode, ProductCategory category,string oldCode)
        {
            if (string.IsNullOrEmpty(newName)) throw new ArgumentNullException("name");
            if (string.IsNullOrEmpty(newCode)) throw new ArgumentNullException("code"); 
            if (newQuantity <= 0) throw new ArgumentOutOfRangeException("quantity");
            if (newPrice <= 0) throw new ArgumentOutOfRangeException("price");
            
            Product product = Products.FirstOrDefault(p => p.Code == oldCode);

            product.Name = newName;
            product.Price = newPrice;
            product.Category = category;
            product.Code = newCode;
            product.Quantity = newQuantity;

            return product.No;
        }
        
        
        //Delete selected product method
        public void DeleteProduct(string code)
        {
            
            Product product = Products.FirstOrDefault(p => p.Code == code);

            if (product == null)
                throw new ArgumentNullException();

            Products.Remove(product);
        } 

        public List<Product> GetProducts(ProductCategory category)
        {
            var products = Products.Where(p => p.Category == category).ToList();

            return products;
        }

        public List<Product> GetProducts(decimal min, decimal max)
        {
            var products = Products.Where(p => p.Price >= min && p.Price <= max).ToList();

            return products;
        }

        public Product GetProducts(string name)
        {
            var product = Products.FirstOrDefault(p => p.Name.ToLower() == name.ToLower());

            return product;
        }

        #endregion

        #region Sale

        public Sale AddSale(DateTime date)
        {
            Sale sale = new Sale()
            {
                Date = date,
                Amount = 0
            };
            Sales.Add(sale);
            
            return sale;
        } //Add sale method

        
        public int AddSaleProduct(Product product,int quantity, double price,Sale sale)
        {
            
            if (price <= 0)
                throw new ArgumentOutOfRangeException("amount");
            
            if (quantity <= 0 && quantity > product.Quantity)
                throw new ArgumentOutOfRangeException("quantity");

            SaleProduct saleProduct = new();

            saleProduct.ProductCode = product;
            saleProduct.Quantity = quantity;
            saleProduct.Price = (decimal) price;
            saleProduct.Sale = sale;
            sale.Amount +=(decimal) (price * quantity) ;
            product.Quantity -= quantity;

            SaleProducts.Add(saleProduct);
            

            return product.No;
        }
        
        public void DeleteSingleSaleProduct(int no , string SaleProductNo)
        {
            var sale = Sales.FirstOrDefault(s => s.No == no);
            
            if (sale == null)
                throw new ArgumentNullException();
            
            var saleItems = SaleProducts.FindAll(s =>s.Sale.No == sale.No && s.ProductCode.Code == SaleProductNo).ToList();

            if (SaleProducts == null)
                throw new ArgumentNullException();

            foreach (var SaleProduct in SaleProducts)
            {
                var product = Products.FirstOrDefault(p => p.Code == SaleProduct.ProductCode.Code);

                product.Quantity += SaleProduct.Quantity;

                sale.Amount -= SaleProduct.Price * SaleProduct.Quantity;
                
                SaleProducts.Remove(SaleProduct);

            }

            
        }
        
        public void DeleteSale(int no)
        {
            
            int sale = Sales.FindIndex(s => s.No == no);
            
            var SaleProduct = SaleProducts.Where(s => s.Sale.No == no).ToList();

            foreach (var salesProduct in SaleProducts)
            {
                var product = Products.FirstOrDefault(p => p.Code == salesProduct.ProductCode.Code);

                product.Quantity += salesProduct.Quantity;

            }

            if (sale == -1)
                throw new ArgumentNullException();

            Sales.RemoveAt(sale);
        }

        public List<Sale> GetSales(DateTime start, DateTime end)
        {
            var sales = Sales.Where(s => s.Date >= start && s.Date <= end).ToList();

            return sales;
        }

        public List<Sale> GetSales(DateTime date)
        {
            var sales = Sales.Where(s => s.Date == date).ToList();

            return sales;
        }

        public List<Sale> GetSales(decimal min, decimal max)
        {
            var sales = Sales.Where(s => s.Amount >= min && s.Amount <= max).ToList();

            return sales;
        }

        #endregion
    }
}