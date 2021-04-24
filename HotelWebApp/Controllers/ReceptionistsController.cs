using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HotelWebApp.Data;
using HotelWebApp.Models;

namespace HotelWebApp.Controllers
{
    public class ReceptionistsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
