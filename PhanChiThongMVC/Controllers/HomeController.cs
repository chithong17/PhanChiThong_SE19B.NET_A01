using Microsoft.AspNetCore.Mvc;
using PhanChiThong_BusinessLogic.Services;
using System.Linq;

namespace PhanChiThongMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly INewsArticleService _newsService;
        public HomeController(INewsArticleService newsService)
        {
            _newsService = newsService;
        }

        public IActionResult Index()
        {
            // Do not need authentication to view active news
            var activeNews = _newsService.GetAll().Where(n => n.NewsStatus == true).OrderByDescending(n => n.CreatedDate).ToList();
            return View(activeNews);
        }

        public IActionResult Details(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();
            var allActive = _newsService.GetAll().Where(n => n.NewsStatus == true).OrderByDescending(n => n.CreatedDate).ToList();
            var article = allActive.FirstOrDefault(n => n.NewsArticleId == id);
            if (article == null) return NotFound();
            
            var articleToUpdate = _newsService.GetById(id);
            if (articleToUpdate != null)
            {
                articleToUpdate.ViewCount = (articleToUpdate.ViewCount ?? 0) + 1;
                _newsService.Update(articleToUpdate);
                article.ViewCount = articleToUpdate.ViewCount; 
            }

            ViewBag.TrendingNews = allActive.Where(n => n.NewsArticleId != id).OrderByDescending(n => n.ViewCount).Take(5).ToList();
            return View(article);
        }

        public IActionResult Search(string query)
        {
            var activeNews = _newsService.GetAll().Where(n => n.NewsStatus == true);
            if (!string.IsNullOrEmpty(query))
            {
                activeNews = activeNews.Where(n => n.NewsTitle.Contains(query, System.StringComparison.OrdinalIgnoreCase) || 
                                                 (n.Headline != null && n.Headline.Contains(query, System.StringComparison.OrdinalIgnoreCase)));
            }
            ViewBag.Query = query;
            return View(activeNews.OrderByDescending(n => n.CreatedDate).ToList());
        }

        public IActionResult AllNews(int page = 1)
        {
            int pageSize = 8;
            var allActive = _newsService.GetAll().Where(n => n.NewsStatus == true).OrderByDescending(n => n.CreatedDate).ToList();
            
            int totalItems = allActive.Count();
            int totalPages = (int)System.Math.Ceiling(totalItems / (double)pageSize);
            
            if (page < 1) page = 1;
            if (page > totalPages && totalPages > 0) page = totalPages;

            var pagedNews = allActive.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;

            return View(pagedNews);
        }
    }
}