using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Jewelry_Store_e.a.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Jewelry_Store_e.a.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(SDMDbContext context) : base(context)
        {
        }

        public async Task<IActionResult> Index(string searchString, string searchcolor, int searchprice)
        {
            var products = from p in _context.Products
                           select p;
            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(s => s.Name.Contains(searchString));
            }
            if (!String.IsNullOrEmpty(searchcolor))
            {
                if (searchcolor.Equals("Gold"))
                {
                    products = products.Where(s => s.Color.Equals(color.Gold));
                }
                if (searchcolor.Equals("Silver"))
                {
                    products = products.Where(s => s.Color.Equals(color.Silver));
                }

            }
            if (searchprice > 0)
            {
                products = products.Where(s => (int)s.price <= (searchprice));
            }
            return View(await products.ToListAsync());
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Customers()
        {
            var customers = from a in _context.Customers select a;
            return View(customers);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
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


