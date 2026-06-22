using PhanChiThong_DataAccess.DAOs;
using PhanChiThong_DataAccess.Models;
using System.Collections.Generic;

namespace PhanChiThong_DataAccess.Repositories
{
    public class SystemAccountRepository : ISystemAccountRepository
    {
        public List<SystemAccount> GetAll() => SystemAccountDAO.Instance.GetAll();
        public SystemAccount GetById(object id) => SystemAccountDAO.Instance.GetById(id);
        public void Add(SystemAccount entity) => SystemAccountDAO.Instance.Add(entity);
        public void Update(SystemAccount entity) => SystemAccountDAO.Instance.Update(entity);
        public void Delete(SystemAccount entity) => SystemAccountDAO.Instance.Delete(entity);
    }
}
