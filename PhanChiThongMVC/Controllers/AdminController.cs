using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhanChiThong_BusinessLogic.Services;
using PhanChiThong_DataAccess.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System;

namespace PhanChiThongMVC.Controllers
{
    public class AdminController : Controller
    {
        private readonly ISystemAccountService _accountService;
        private readonly INewsArticleService _newsArticleService;
        private readonly ICategoryService _categoryService;
        public AdminController(ISystemAccountService accountService, INewsArticleService newsArticleService, ICategoryService categoryService)
        {
            _accountService = accountService;
            _newsArticleService = newsArticleService;
            _categoryService = categoryService;
        }

        private bool IsAdmin() => HttpContext.Session.GetInt32("Role") == 0;

        public IActionResult Index()
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");
            return View(_accountService.GetAll());
        }

        [HttpPost]
        public IActionResult SaveAccount(SystemAccount account)
        {
            if (!IsAdmin()) return Unauthorized();

            if (string.IsNullOrWhiteSpace(account.AccountName) || string.IsNullOrWhiteSpace(account.AccountEmail) || string.IsNullOrWhiteSpace(account.AccountPassword))
            {
                TempData["ErrorMessage"] = "Please fill in all required fields (Name, Email, Password).";
                return RedirectToAction("Index");
            }
            var existing = _accountService.GetById(account.AccountId);
            if (existing == null)
            {
                var all = _accountService.GetAll();
                // Identity column will handle ID generation
                _accountService.Add(account);
            }
            else
            {
                existing.AccountName = account.AccountName;
                existing.AccountEmail = account.AccountEmail;
                existing.AccountRole = account.AccountRole;
                existing.AccountPassword = account.AccountPassword;
                _accountService.Update(existing);
            }
            return RedirectToAction("Index");
        }

        public IActionResult DeleteAccount(short id)
        {
            if (!IsAdmin()) return Unauthorized();
            var acc = _accountService.GetById(id);
            if (acc != null) _accountService.Delete(acc);
            return RedirectToAction("Index");
        }

        public IActionResult Report(DateTime? startDate, DateTime? endDate)
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");
            var articles = _newsArticleService.GetAll();
            if (startDate.HasValue) articles = articles.Where(a => a.CreatedDate >= startDate.Value).ToList();
            if (endDate.HasValue) {
                var nextDay = endDate.Value.AddDays(1);
                articles = articles.Where(a => a.CreatedDate < nextDay).ToList();
            }
            articles = articles.OrderByDescending(a => a.CreatedDate).ToList();
            ViewBag.StartDate = startDate?.ToString("yyyy-MM-dd");
            ViewBag.EndDate = endDate?.ToString("yyyy-MM-dd");
            return View(articles);
        }

        public IActionResult ExportReport(DateTime? startDate, DateTime? endDate)
        {
            if (!IsAdmin()) return Unauthorized();

            var articles = _newsArticleService.GetAll();
            if (startDate.HasValue) articles = articles.Where(a => a.CreatedDate >= startDate.Value).ToList();
            if (endDate.HasValue) {
                var nextDay = endDate.Value.AddDays(1);
                articles = articles.Where(a => a.CreatedDate < nextDay).ToList();
            }
            articles = articles.OrderByDescending(a => a.CreatedDate).ToList();

            var builder = new System.Text.StringBuilder();
            builder.AppendLine("ID,Title,Created Date,Status,Views");

            foreach (var item in articles)
            {
                var id = item.NewsArticleId;
                // Escape commas in title by wrapping in quotes
                var title = item.NewsTitle != null ? $"\"{item.NewsTitle.Replace("\"", "\"\"")}\"" : "";
                var date = item.CreatedDate?.ToString("yyyy-MM-dd HH:mm");
                var status = item.NewsStatus == true ? "Active" : "Inactive";
                var views = item.ViewCount ?? 0;
                
                builder.AppendLine($"{id},{title},{date},{status},{views}");
            }

            return File(System.Text.Encoding.UTF8.GetBytes(builder.ToString()), "text/csv", "NewsReport.csv");
        }

        public IActionResult Dashboard()
        {
            if (!IsAdmin()) return Unauthorized();

            // 1. Calculate KPIs
            var allArticles = _newsArticleService.GetAll();
            var totalArticles = allArticles.Count;
            var totalViews = allArticles.Sum(a => a.ViewCount) ?? 0;
            var totalStaffs = _accountService.GetAll().Count(a => a.AccountRole == 1);
            var totalCategories = _categoryService.GetAll().Count;

            ViewBag.TotalArticles = totalArticles;
            ViewBag.TotalViews = totalViews;
            ViewBag.TotalStaffs = totalStaffs;
            ViewBag.TotalCategories = totalCategories;

            // 2. Pie Chart: Views by Category
            var viewsByCategory = _newsArticleService.GetViewsByCategory();
            
            ViewBag.PieLabels = System.Text.Json.JsonSerializer.Serialize(viewsByCategory.Keys);
            ViewBag.PieData = System.Text.Json.JsonSerializer.Serialize(viewsByCategory.Values);

            // 3. Bar Chart: Articles by Staff
            var articlesByStaff = _newsArticleService.GetArticlesByStaff();

            ViewBag.BarLabels = System.Text.Json.JsonSerializer.Serialize(articlesByStaff.Keys);
            ViewBag.BarData = System.Text.Json.JsonSerializer.Serialize(articlesByStaff.Values);

            return View();
        }
    }
}