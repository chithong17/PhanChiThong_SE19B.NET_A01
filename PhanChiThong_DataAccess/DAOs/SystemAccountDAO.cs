using PhanChiThong_DataAccess.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace PhanChiThong_DataAccess.DAOs
{
    public class SystemAccountDAO
    {
        private static SystemAccountDAO instance = null;
        private static readonly object instanceLock = new object();
        private SystemAccountDAO() { }
        public static SystemAccountDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new SystemAccountDAO();
                    }
                    return instance;
                }
            }
        }

        public List<SystemAccount> GetAll()
        {
            using var context = new FUNewsManagementContext();
            return context.SystemAccounts.ToList();
        }

        public SystemAccount GetById(object id)
        {
            using var context = new FUNewsManagementContext();
            return context.SystemAccounts.Find(id);
        }

        public void Add(SystemAccount entity)
        {
            using var context = new FUNewsManagementContext();
            context.SystemAccounts.Add(entity);
            context.SaveChanges();
        }

        public void Update(SystemAccount entity)
        {
            using var context = new FUNewsManagementContext();
            context.SystemAccounts.Update(entity);
            context.SaveChanges();
        }

        public void Delete(SystemAccount entity)
        {
            using var context = new FUNewsManagementContext();
            context.SystemAccounts.Remove(entity);
            context.SaveChanges();
        }
    }
}
