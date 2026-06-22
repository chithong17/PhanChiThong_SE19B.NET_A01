using Microsoft.AspNetCore.Mvc;
using PhanChiThong_BusinessLogic.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace PhanChiThongMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly ISystemAccountService _accountService;
        private readonly IConfiguration _configuration;

        public AccountController(ISystemAccountService accountService, IConfiguration configuration)
        {
            _accountService = accountService;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetInt32("Role") != null)
            {
                return RedirectToAction("Index", "Home"); 
            }
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ViewBag.Error = "Please enter both Email and Password.";
                return View();
            }

            var adminEmail = _configuration["AdminAccount:Email"];
            var adminPassword = _configuration["AdminAccount:Password"];

            if (email == adminEmail && password == adminPassword)
            {
                HttpContext.Session.SetInt32("Role", 0); // 0 for Admin
                HttpContext.Session.SetString("Email", email);
                return RedirectToAction("Index", "Home");
            }

            var accounts = _accountService.GetAll();
            var user = accounts.FirstOrDefault(a => a.AccountEmail == email && a.AccountPassword == password);

            if (user != null)
            {
                HttpContext.Session.SetInt32("Role", user.AccountRole ?? -1);
                HttpContext.Session.SetString("Email", user.AccountEmail);
                HttpContext.Session.SetInt32("AccountId", user.AccountId);
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Invalid email or password.";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
