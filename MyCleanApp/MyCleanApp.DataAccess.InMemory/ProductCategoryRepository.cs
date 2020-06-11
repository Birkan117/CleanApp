using MyCleanApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyCleanApp.DataAccess.InMemory
{
    public class ProductCategoryRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<ProductCategory> productCategories;
        //This is a constructor bellow
        public ProductCategoryRepository()
        {
            productCategories = cache["productCategories"] as List<ProductCategory>;
            if (productCategories == null)
            {
                productCategories = new List<ProductCategory>();
            }
        }
        //when people add products to the repository, we dont want it to add it straight away. They have to be specific
        //The commit bellow will save the list of products into the cache.
        public void Commit()
        {
            cache["productCategories"] = productCategories;
        }
        //Adds the product to the product list
        public void Insert(ProductCategory p)
        {
            productCategories.Add(p);
        }

        public void Update(ProductCategory productCategory)
        {
            //Looks inside the database to find the product that we want to update.
            ProductCategory productCategoryToUpdate = productCategories.Find(p => p.Id == productCategory.Id);

            if (productCategoryToUpdate != null)
            {
                productCategoryToUpdate = productCategory;
            }
            else
            {
                throw new Exception("Product Category Not Found");
            }
        }

        public ProductCategory Find(string Id)
        {
            ProductCategory productCategory = productCategories.Find(p => p.Id == Id);

            if (productCategory != null)
            {
                return productCategory;
            }
            else
            {
                throw new Exception("Product Category Not Found");
            }

        }
        //This will return a list that can be queried "hence the IQueryable"
        public IQueryable<ProductCategory> Collection()
        {
            return productCategories.AsQueryable();
        }

        public void Delete(string Id)
        {
            //Looks inside the database to find the product that we want to update.
            ProductCategory productCategoryToDelete = productCategories.Find(p => p.Id == Id);

            if (productCategoryToDelete != null)
            {
                productCategories.Remove(productCategoryToDelete);
            }
            else
            {
                throw new Exception("Product Category Not Found");
            }

        }
}
}
