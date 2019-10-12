using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jewelry_Store_e.a.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jewelry_Store_e.a.Controllers
{
    [Route("api/graf")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class GrafAPIController : BaseController
    {
        public GrafAPIController(SDMDbContext context) : base(context)
        {
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("dates")]
        public async Task<ActionResult<Dictionary<DateTime, int>>> OrdersDate()
        {
            return _context.Orders.GroupBy(o => o.OrderDate.Date).Select(a => new { name = a.Key.Date, count = a.Sum(c => c.PurchaseProducts.Count) }).ToDictionary(g => g.name.Date, g => g.count);
        }

        /*[HttpGet("Title")]
        public async Task<ActionResult<Dictionary<title, int>>> ProductsInventory()
        {
            return _context.Products.GroupBy(p => p.Title).Select(a => new { name = a.Key, count = a.Count() }).ToDictionary(g => g.name, g => g.count);
        }
		//****************************************or*/
        [HttpGet("title")]
        public async Task<ActionResult<Dictionary<string, int>>> ProductsInventory()
        {
            return _context.Products.GroupBy(p => p.Title).Select(a => new { name = a.Key, count = a.Count() }).ToDictionary(g => g.name.ToString(), g => g.count);
        }
    }
}