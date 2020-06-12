using MyCleanApp.Core.Contracts;
using MyCleanApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCleanApp.DataAccess.SQL
{
    public class SQLRepository<T> : IRepository<T> where T : BaseEntity
    {
        //To make this repository work we need to inject in our DataContext class.
        internal DataContext context;
        //DbSet is the underlying table we would like to access.
        internal DbSet<T> dbSet;

        public SQLRepository(DataContext context)
        {
            this.context = context;
            //Here we are passing in the model to the database so the database knows to use the table that the
            //...model is relating to. I.E. the Product model will be using the Product table in the database.
            this.dbSet = context.Set<T>();
        }
        public IQueryable<T> Collection()
        {
            return dbSet;
        }

        public void Commit()
        {
            //DataContext enherits DbContext class which has its own methodoligies, which includes the SaveChanges. 
            //...This is part of entity framework.
            context.SaveChanges();
        }

        public void Delete(string Id)
        {
            var t = Find(Id);
            if (context.Entry(t).State == EntityState.Detached)
                dbSet.Attach(t);
            dbSet.Remove(t);
        }

        public T Find(string Id)
        {
            //dbSet has its own find methodology
            return dbSet.Find(Id);
        }

        public void Insert(T t)
        {
            //dbSet has its own Insert/Add methodology
            dbSet.Add(t);
        }

        public void Update(T t)
        {
            //"Attach" we are attaching the entity we want to update.
            //...So we are passing in an object "t" and attaching it to entity framework table.
            dbSet.Attach(t);
            context.Entry(t).State = EntityState.Modified;
            
        }
    }
}
