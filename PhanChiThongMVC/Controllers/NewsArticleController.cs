using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhanChiThong_BusinessLogic.Services;
using PhanChiThong_DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Collections.Generic;

namespace PhanChiThongMVC.Controllers
{
    public class NewsArticleController : Controller
    {
        private readonly INewsArticleService _newsService;
        private readonly ICategoryService _catService;
        private readonly ITagService _tagService;
        private readonly IWebHostEnvironment _env;
        
        public NewsArticleController(INewsArticleService newsService, ICategoryService catService, ITagService tagService, IWebHostEnvironment env)
        {
            _newsService = newsService;
            _catService = catService;
            _tagService = tagService;
            _env = env;
        }

        private bool IsStaff() => HttpContext.Session.GetInt32("Role") == 1;

        public IActionResult Index(string searchTitle)
        {
            if (!IsStaff()) return RedirectToAction("Login", "Account");
            ViewBag.Categories = _catService.GetAll();
            ViewBag.AllTags = _tagService.GetAll();
            
            var list = _newsService.GetAll();
            if (!string.IsNullOrEmpty(searchTitle))
                list = list.Where(n => n.NewsTitle.Contains(searchTitle, StringComparison.OrdinalIgnoreCase)).ToList();
            
            list = list.OrderBy(n => int.TryParse(n.NewsArticleId, out int val) ? val : int.MaxValue).ToList();
            
            return View(list);
        }

        [HttpPost]
        public async Task<IActionResult> Save(PhanChiThong_DataAccess.Models.NewsArticle article, List<int> TagIds, IFormFile ImageFile, bool IsUpdate = false)
        {
            if (!IsStaff()) return Unauthorized();

            if (string.IsNullOrWhiteSpace(article.NewsArticleId) || string.IsNullOrWhiteSpace(article.NewsTitle) || string.IsNullOrWhiteSpace(article.Headline))
            {
                TempData["ErrorMessage"] = "Please fill in all required fields (ID, Title, Headline).";
                return RedirectToAction("Index");
            }
            
            if (ImageFile != null && ImageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(_env.WebRootPath, "images", "news");
                if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);
                
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + ImageFile.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(stream);
                }
                
                article.ImageUrl = "/images/news/" + uniqueFileName;
            }

            var selectedTags = _tagService.GetAll().Where(t => TagIds.Contains(t.TagId)).ToList();

            if (!IsUpdate)
            {
                var existing = _newsService.GetById(article.NewsArticleId);
                if (existing != null)
                {
                    TempData["ErrorMessage"] = $"Article ID '{article.NewsArticleId}' already exists. Please choose another ID.";
                    return RedirectToAction("Index");
                }
                
                article.CreatedDate = DateTime.Now;
                article.CreatedById = (short)HttpContext.Session.GetInt32("AccountId").Value;
                article.Tags = selectedTags;
                _newsService.Add(article);
            }
            else
            {
                var existing = _newsService.GetById(article.NewsArticleId);
                if (existing == null) 
                {
                    TempData["ErrorMessage"] = "Article not found or has been deleted.";
                    return RedirectToAction("Index");
                }

                article.Tags = selectedTags;
                article.ModifiedDate = DateTime.Now;
                article.UpdatedById = (short)HttpContext.Session.GetInt32("AccountId").Value;
                article.ViewCount = existing.ViewCount;
                
                _newsService.Update(article);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Delete(string id)
        {
            if (!IsStaff()) return Unauthorized();
            var item = _newsService.GetById(id);
            if (item != null) _newsService.Delete(item);
            return RedirectToAction("Index");
        }
    }
}