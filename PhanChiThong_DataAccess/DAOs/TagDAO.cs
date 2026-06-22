using PhanChiThong_DataAccess.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace PhanChiThong_DataAccess.DAOs
{
    public class TagDAO
    {
        private static TagDAO instance = null;
        private static readonly object instanceLock = new object();
        private TagDAO() { }
        public static TagDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new TagDAO();
                    }
                    return instance;
                }
            }
        }

        public List<Tag> GetAll()
        {
            using var context = new FUNewsManagementContext();
            return context.Tags.ToList();
        }

        public Tag GetById(object id)
        {
            using var context = new FUNewsManagementContext();
            return context.Tags.Find(id);
        }

        public void Add(Tag entity)
        {
            using var context = new FUNewsManagementContext();
            context.Tags.Add(entity);
            context.SaveChanges();
        }

        public void Update(Tag entity)
        {
            using var context = new FUNewsManagementContext();
            context.Tags.Update(entity);
            context.SaveChanges();
        }

        public void Delete(Tag entity)
        {
            using var context = new FUNewsManagementContext();
            context.Tags.Remove(entity);
            context.SaveChanges();
        }
    }
}
