using Microsoft.AspNetCore.Mvc;
using PhanChiThong_BusinessLogic.Services;
using PhanChiThong_DataAccess.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace PhanChiThongMVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly INewsArticleService _newsArticleService;
        public CategoryController(ICategoryService categoryService, INewsArticleService newsArticleService)
        {
            _categoryService = categoryService;
            _newsArticleService = newsArticleService;
        }

        private bool IsStaff() => HttpContext.Session.GetInt32("Role") == 1;

        public IActionResult Index()
        {
            if (!IsStaff()) return RedirectToAction("Login", "Account");
            return View(_categoryService.GetAll());
        }

        [HttpPost]
        public IActionResult Save(Category category)
        {
            if (!IsStaff()) return Unauthorized();

            if (string.IsNullOrWhiteSpace(category.CategoryName) || string.IsNullOrWhiteSpace(category.CategoryDesciption))
            {
                TempData["ErrorMessage"] = "Please fill in all required fields (Name, Description).";
                return RedirectToAction("Index");
            }
            var existing = _categoryService.GetById(category.CategoryId);
            if (existing == null)
            {
                var all = _categoryService.GetAll();
                // Identity column will handle ID generation

                _categoryService.Add(category);
            }
            else
            {
                existing.CategoryName = category.CategoryName;
                existing.CategoryDesciption = category.CategoryDesciption;
                existing.IsActive = category.IsActive;
                _categoryService.Update(existing);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Delete(short id)
        {
            if (!IsStaff()) return Unauthorized();
            var articles = _newsArticleService.GetAll().Any(a => a.CategoryId == id);
            if (articles)
            {
                TempData["Error"] = "Cannot delete category because it is already stored in a news article.";
                return RedirectToAction("Index");
            }
            var cat = _categoryService.GetById(id);
            if (cat != null) _categoryService.Delete(cat);
            return RedirectToAction("Index");
        }
    }
}