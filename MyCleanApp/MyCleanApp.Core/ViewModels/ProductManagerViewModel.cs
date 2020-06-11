using MyCleanApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCleanApp.Core.ViewModels
{
    public class ProductManagerViewModel
    {
        public Product Product { get; set; }
        //A list you can itterate through (IEnumerable)
        public IEnumerable<ProductCategory> ProductCategories { get; set; }
    }
}
