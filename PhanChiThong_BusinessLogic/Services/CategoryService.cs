using PhanChiThong_DataAccess.Models;
using PhanChiThong_DataAccess.Repositories;
using System.Collections.Generic;

namespace PhanChiThong_BusinessLogic.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;

        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public List<Category> GetAll() => _repository.GetAll();
        public Category GetById(object id) => _repository.GetById(id);
        public void Add(Category entity) => _repository.Add(entity);
        public void Update(Category entity) => _repository.Update(entity);
        public void Delete(Category entity) => _repository.Delete(entity);
    }
}
