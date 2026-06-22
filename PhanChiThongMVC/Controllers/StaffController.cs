using Microsoft.AspNetCore.Mvc;
using PhanChiThong_BusinessLogic.Services;
using PhanChiThong_DataAccess.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace PhanChiThongMVC.Controllers
{
    public class StaffController : Controller
    {
        private readonly ISystemAccountService _accountService;
        private readonly INewsArticleService _newsService;
        public StaffController(ISystemAccountService accountService, INewsArticleService newsService)
        {
            _accountService = accountService;
            _newsService = newsService;
        }

        private bool IsStaff() => HttpContext.Session.GetInt32("Role") == 1;

        public IActionResult Profile()
        {
            if (!IsStaff()) return RedirectToAction("Login", "Account");
            int id = HttpContext.Session.GetInt32("AccountId").Value;
            var account = _accountService.GetById((short)id);
            if (account != null) account.AccountPassword = ""; // Don't send password to HTML
            return View(account);
        }

        [HttpPost]
        public IActionResult UpdateProfile(SystemAccount model)
        {
            if (!IsStaff()) return Unauthorized();
            short realId = (short)HttpContext.Session.GetInt32("AccountId").Value;
            var existing = _accountService.GetById(realId);
            if (existing != null)
            {
                existing.AccountName = model.AccountName;
                existing.AccountEmail = model.AccountEmail;
                if (!string.IsNullOrEmpty(model.AccountPassword))
                {
                    existing.AccountPassword = model.AccountPassword;
                }
                _accountService.Update(existing);
                ViewBag.Message = "Profile updated successfully!";
            }
            return View("Profile", existing);
        }

        public IActionResult History()
        {
            if (!IsStaff()) return RedirectToAction("Login", "Account");
            int id = HttpContext.Session.GetInt32("AccountId").Value;
            var history = _newsService.GetAll().Where(n => n.CreatedById == id).OrderByDescending(n => n.CreatedDate).ToList();
            return View(history);
        }
    }
}