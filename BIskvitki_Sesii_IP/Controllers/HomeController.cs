using BIskvitki_Sesii_IP.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BIskvitki_Sesii_IP.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            // Check if there's a UserPreference cookie
            Request.Cookies.TryGetValue("UserPreference", out string userPreference);
            ViewBag.UserPreference = userPreference;

            if (string.IsNullOrEmpty(userPreference))
            {
                ViewBag.UserPreference = "Default"; // Set default theme if no cookie is found
            }

            ViewBag.Username = HttpContext.Session.GetString("Username"); // Retrieve username from the session
            return View();
        }

        public IActionResult SetCookie(string preference)
        {
            Response.Cookies.Append("UserPreference", preference, new CookieOptions
            {
                Expires = DateTime.Now.AddDays(7),
                HttpOnly = true,
                IsEssential = true
            });
            return RedirectToAction("Index");
        }

        // Storing the user name in the session
        [HttpPost]
        public IActionResult SetUsername(string username)
        {
            HttpContext.Session.SetString("Username", username);
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
