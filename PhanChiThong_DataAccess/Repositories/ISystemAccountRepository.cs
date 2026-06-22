using PhanChiThong_DataAccess.Models;
using System.Collections.Generic;

namespace PhanChiThong_DataAccess.Repositories
{
    public interface ISystemAccountRepository
    {
        List<SystemAccount> GetAll();
        SystemAccount GetById(object id);
        void Add(SystemAccount entity);
        void Update(SystemAccount entity);
        void Delete(SystemAccount entity);
    }
}
