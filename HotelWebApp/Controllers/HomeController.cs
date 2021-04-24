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
            if (User.HasClaim("Receptionist", "IsReceptionist"))
            {
                return RedirectToAction(nameof(Index), "Receptionists");
            }

            if (User.HasClaim("Waiter", "IsWaiter"))
            {
                return RedirectToAction(nameof(Index), "Waiters");
            }

            if (User.HasClaim("Chef", "IsChef"))
            {
                return RedirectToAction(nameof(Index), "Chefs");
            }

            else
            {
                TempData["Security Check"] = "failed";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
