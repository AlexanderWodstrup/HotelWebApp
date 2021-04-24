using HotelWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace HotelWebApp.Controllers
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
            if (User.HasClaim("LaundryUser", "IsLaundryUser"))
            {
                return RedirectToAction(nameof(Index), "LaundryUser");
            }

            if (User.HasClaim("UserAdmin", "IsUserAdmin"))
            {
                return RedirectToAction(nameof(Index), "UserAdmin");
            }

            if (User.HasClaim("SystemAdmin", "IsSystemAdmin"))
            {
                return RedirectToAction(nameof(Index), "SystemAdmin");
            }

            else
            {
                TempData["Security Check"] = "failed";
                return RedirectToAction(nameof(Index));
            }
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
