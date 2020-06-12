using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyCleanApp.Core.Contracts;
using MyCleanApp.Core.Models;
using MyCleanApp.Core.ViewModels;
using MyCleanApp.DataAccess.InMemory;

namespace MyCleanApp.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        //Need To first create an instance of the product repository
        //ProductRepository context;
        //ProductCategoryRepository productCategories;


        //InMemoryRepository<Product> context;
        //InMemoryRepository<ProductCategory> productCategories;
        
        //The Code will now use IRepository instead (Due to dependancy injection)
        IRepository<Product> context;
        IRepository<ProductCategory> productCategories;


        //Then create a constructor of the productManagerController that initialises the product repository
        //Injecting the classes through the constructor bellow. "IRepository<>"
        public ProductManagerController(IRepository<Product> productContext, IRepository<ProductCategory> productCategoryContext )
        {
            context = productContext;
            productCategories = productCategoryContext;
        }
        // GET: ProductManager
        public ActionResult Index()
        {
            List<Product> products = context.Collection().ToList();
            return View(products);
        }

        //Method to create the product
        public ActionResult Create()
        {
            ProductManagerViewModel viewModel = new ProductManagerViewModel();
            viewModel.Product = new Product();
            //Gets the product categories from the database
            viewModel.ProductCategories = productCategories.Collection();

            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Create(Product product, HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            else
            {
                if (file!=null)
                {
                    product.Image = product.Id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//Content//ProductImages//") + product.Image);
                }

                context.Insert(product);
                context.Commit();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string Id)
        {
            Product product = context.Find(Id);
            if (product==null)
            {
                return HttpNotFound();
            }
            else
            {
                ProductManagerViewModel viewModel = new ProductManagerViewModel();
                viewModel.Product = product;
                //Gets the product categories from the database
                viewModel.ProductCategories = productCategories.Collection();
                return View(viewModel);

            }
        }
        [HttpPost]
        public ActionResult Edit(Product product, string Id, HttpPostedFileBase file)
        {
            Product productToEdit = context.Find(Id);
            if (productToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(product);
                }
                if (file != null)
                {
                    productToEdit.Image = product.Id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//Content//ProductImages//") + productToEdit.Image);
                }
                productToEdit.Categaroy = product.Categaroy;
                productToEdit.Description = product.Description;
                productToEdit.Name = product.Name;
                productToEdit.Price = product.Price;

                context.Commit();

                return RedirectToAction("Index");
            }
        }
        public ActionResult Delete(string Id)
        {
            Product productToDelete = context.Find(Id);
            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            { 
                return View(productToDelete);
            }
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id) 
        {
            Product productToDelete = context.Find(Id);
            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(Id);
                context.Commit();
                return RedirectToAction("Index");
            }
        }
    }
}