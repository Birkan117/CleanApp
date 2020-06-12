using MyCleanApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCleanApp.DataAccess.SQL
{
    public class DataContext : DbContext
    {
        //First create constructor to capture and pass through the connection string the base class is expecting
        public DataContext(): base("DefaultConnection")
        {

        }
        //Telling the datacontext what models we want stored into the database.
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }

    }
}
