using PhanChiThong_DataAccess.DAOs;
using PhanChiThong_DataAccess.Models;
using System.Collections.Generic;

namespace PhanChiThong_DataAccess.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        public List<Category> GetAll() => CategoryDAO.Instance.GetAll();
        public Category GetById(object id) => CategoryDAO.Instance.GetById(id);
        public void Add(Category entity) => CategoryDAO.Instance.Add(entity);
        public void Update(Category entity) => CategoryDAO.Instance.Update(entity);
        public void Delete(Category entity) => CategoryDAO.Instance.Delete(entity);
    }
}
