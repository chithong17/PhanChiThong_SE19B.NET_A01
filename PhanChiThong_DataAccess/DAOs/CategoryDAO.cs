using PhanChiThong_DataAccess.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace PhanChiThong_DataAccess.DAOs
{
    public class CategoryDAO
    {
        private static CategoryDAO instance = null;
        private static readonly object instanceLock = new object();
        private CategoryDAO() { }
        public static CategoryDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new CategoryDAO();
                    }
                    return instance;
                }
            }
        }

        public List<Category> GetAll()
        {
            using var context = new FUNewsManagementContext();
            return context.Categories.ToList();
        }

        public Category GetById(object id)
        {
            using var context = new FUNewsManagementContext();
            return context.Categories.Find(id);
        }

        public void Add(Category entity)
        {
            using var context = new FUNewsManagementContext();
            context.Categories.Add(entity);
            context.SaveChanges();
        }

        public void Update(Category entity)
        {
            using var context = new FUNewsManagementContext();
            context.Categories.Update(entity);
            context.SaveChanges();
        }

        public void Delete(Category entity)
        {
            using var context = new FUNewsManagementContext();
            context.Categories.Remove(entity);
            context.SaveChanges();
        }
    }
}
