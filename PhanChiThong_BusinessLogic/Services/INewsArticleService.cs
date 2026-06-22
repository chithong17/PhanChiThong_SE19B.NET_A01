using PhanChiThong_DataAccess.Models;
using System.Collections.Generic;

namespace PhanChiThong_BusinessLogic.Services
{
    public interface INewsArticleService
    {
        List<NewsArticle> GetAll();
        NewsArticle GetById(object id);
        void Add(NewsArticle entity);
        void Update(NewsArticle entity);
        void Delete(NewsArticle entity);
        Dictionary<string, int> GetViewsByCategory();
        Dictionary<string, int> GetArticlesByStaff();
    }
}
