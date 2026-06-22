using PhanChiThong_DataAccess.DAOs;
using PhanChiThong_DataAccess.Models;
using System.Collections.Generic;

namespace PhanChiThong_DataAccess.Repositories
{
    public class NewsArticleRepository : INewsArticleRepository
    {
        public List<NewsArticle> GetAll() => NewsArticleDAO.Instance.GetAll();
        public NewsArticle GetById(object id) => NewsArticleDAO.Instance.GetById(id);
        public void Add(NewsArticle entity) => NewsArticleDAO.Instance.Add(entity);
        public void Update(NewsArticle entity) => NewsArticleDAO.Instance.Update(entity);
        public void Delete(NewsArticle entity) => NewsArticleDAO.Instance.Delete(entity);
        public Dictionary<string, int> GetViewsByCategory() => NewsArticleDAO.Instance.GetViewsByCategory();
        public Dictionary<string, int> GetArticlesByStaff() => NewsArticleDAO.Instance.GetArticlesByStaff();
    }
}
