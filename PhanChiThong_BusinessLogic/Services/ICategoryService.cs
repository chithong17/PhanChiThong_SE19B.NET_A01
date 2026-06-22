using PhanChiThong_DataAccess.Models;
using System.Collections.Generic;

namespace PhanChiThong_BusinessLogic.Services
{
    public interface ICategoryService
    {
        List<Category> GetAll();
        Category GetById(object id);
        void Add(Category entity);
        void Update(Category entity);
        void Delete(Category entity);
    }
}
