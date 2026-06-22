using PhanChiThong_DataAccess.Models;
using PhanChiThong_DataAccess.Repositories;
using System.Collections.Generic;

namespace PhanChiThong_BusinessLogic.Services
{
    public class SystemAccountService : ISystemAccountService
    {
        private readonly ISystemAccountRepository _repository;

        public SystemAccountService(ISystemAccountRepository repository)
        {
            _repository = repository;
        }

        public List<SystemAccount> GetAll() => _repository.GetAll();
        public SystemAccount GetById(object id) => _repository.GetById(id);
        public void Add(SystemAccount entity) => _repository.Add(entity);
        public void Update(SystemAccount entity) => _repository.Update(entity);
        public void Delete(SystemAccount entity) => _repository.Delete(entity);
    }
}
