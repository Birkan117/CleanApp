using MyCleanApp.Core.Contracts;
using MyCleanApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyCleanApp.WebUI.Controllers
{
    public class HomeController : Controller
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
        public HomeController(IRepository<Product> productContext, IRepository<ProductCategory> productCategoryContext)
        {
            context = productContext;
            productCategories = productCategoryContext;
        }
        public ActionResult Index()
        {
            List<Product> products = context.Collection().ToList();
            return View(products);
        }

        public ActionResult Details(string Id)
        {
            Product product = context.Find(Id);
            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(product);
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}