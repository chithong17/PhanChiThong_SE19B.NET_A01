using PhanChiThong_DataAccess.Models;
using PhanChiThong_DataAccess.Repositories;
using System.Collections.Generic;

namespace PhanChiThong_BusinessLogic.Services
{
    public class NewsArticleService : INewsArticleService
    {
        private readonly INewsArticleRepository _repository;

        public NewsArticleService(INewsArticleRepository repository)
        {
            _repository = repository;
        }

        public List<NewsArticle> GetAll() => _repository.GetAll();
        public NewsArticle GetById(object id) => _repository.GetById(id);
        public void Add(NewsArticle entity) => _repository.Add(entity);
        public void Update(NewsArticle entity) => _repository.Update(entity);
        public void Delete(NewsArticle entity) => _repository.Delete(entity);

        public Dictionary<string, int> GetViewsByCategory() => _repository.GetViewsByCategory();
        public Dictionary<string, int> GetArticlesByStaff() => _repository.GetArticlesByStaff();
    }
}
