using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyCleanApp.Core;
using MyCleanApp.Core.Models;

namespace MyCleanApp.DataAccess.InMemory
{
    public class ProductRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<Product> products;
        //This is a constructor bellow
        public ProductRepository()
        {
            products = cache["products"] as List<Product>;
            if (products == null)
            {
                products = new List<Product>();
            }
        }
        //when people add products to the repository, we dont want it to add it straight away. They have to be specific
        //The commit bellow will save the list of products into the cache.
        public void Commit()
        {
            cache["products"] = products;
        }
        //Adds the product to the product list
        public void Insert(Product p)
        {
            products.Add(p);
        }

        public void Update(Product product)
        {
            //Looks inside the database to find the product that we want to update.
            Product productToUpdate = products.Find(p => p.Id == product.Id);

            if (productToUpdate != null)
            {
                productToUpdate = product;
            }
            else
            {
                throw new Exception("Product Not Found");
            }
        }

        public Product Find(string Id)
        {
            Product product = products.Find(p => p.Id == Id);

            if (product != null)
            {
                return product;
            }
            else
            {
                throw new Exception("Product Not Found");
            }

        }
        //This will return a list that can be queried "hence the IQueryable"
        public IQueryable<Product> Collection()
        {
            return products.AsQueryable();
        }

        public void Delete(string Id)
        {
            //Looks inside the database to find the product that we want to update.
            Product productToDelete = products.Find(p => p.Id == Id);

            if (productToDelete!= null)
            {
                products.Remove(productToDelete);
            }
            else
            {
                throw new Exception("Product Not Found");
            }

        }
    }
}
