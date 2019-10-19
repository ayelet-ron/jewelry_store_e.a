using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Jewelry_Store_e.a.Controllers
{
    public class APIViewsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GrafInventory()
        {
            return View();
        }
        public IActionResult GrafDates()
        {
            return View();
        }
    }
}