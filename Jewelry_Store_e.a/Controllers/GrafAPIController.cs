using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Jewelry_Store_e.a.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Extensions.Internal;

namespace Jewelry_Store_e.a.Controllers
{
    [Route("api/graf")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class GrafAPIController : BaseController
    {
        public GrafAPIController(JewelryContext context) : base(context)
        {
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("dates")]
        /*public async Task<ActionResult<List<Tuple<DateTime, int>>>> OrdersDate()
        {
            return _context.Orders.GroupBy(o => o.OrderDate.Date).Select(a => new Tuple<DateTime, int>({ "date": a.Key.Date, "count": a.Sum(c => c.PurchaseProducts.Count) })).ToListAsync<Tuple<DateTime, int>>(g =>new Tuple (date = g.name.Date, count = g => g.count));
        }*/

        public async Task<ActionResult<List<Tuple<DateTime, int>>>> OrdersDate()
        {
            return _context.Orders.GroupBy(o => o.OrderDate.Date).Select(a => new { name = a.Key.Date, count = a.Sum(c => c.PurchaseProducts.Count) }).AsEnumerable().Select(c => new Tuple<DateTime, int>(c.name.Date, c.count)).ToList();
        }

        /*[HttpGet("Title")]
        public async Task<ActionResult<Dictionary<title, int>>> ProductsInventory()
        {
            return _context.Products.GroupBy(p => p.Title).Select(a => new { name = a.Key, count = a.Count() }).ToDictionary(g => g.name, g => g.count);
        }
		//****************************************or*/
        [HttpGet("title")]
        // public async Task<ActionResult<List<Tuple<title,IEnumerable<Tuple<color,int>>>>>> ProductsInventory()
        public async Task<ActionResult<List<Tuple<string, IEnumerable<Tuple<string, int>>>>>> ProductsInventory()
        {
            var m = _context.Products.GroupBy(p => p.Title).Select(g => Tuple.Create(g.Key.ToString(), g.GroupBy(x => x.Color).Select(cg => Tuple.Create(cg.Key.ToString(), cg.Count())))).ToList();
            /* {
                Title = g.Key,
                Color = 
            }).ToList();
               {
                    Color = cg.Count()
                })
            }).ToList();*/
            return m;
            /*List<Tuple<string, Tuple<int, int>>> final=null;  
            int count_silver = 0;
            int count_gold = 0;
            var grouped = _context.Products.GroupBy(p => new { p.Title, p.Color }).GroupBy(p => p.Key.Title);
            foreach ( var t in grouped)
            {
                foreach (var c in t)
                {
                    if(c.Equals(color.Gold))
                    {
                        count_gold++;
                    }
                    else
                    {
                        count_silver++;
                    }
                }
                final.Add(new Tuple<string, Tuple<int, int>>(t.ToString(),new Tuple<int,int>(count_gold, count_silver)));
            }
            Tuple<title, Tuple<int, int>> fin = null;

            return final;
            //return _context.Products.GroupBy(p => new { p.Title, p.Color }).GroupBy(p=>p.Key.Title).Select(a => new { name = a.Key, count = new Tuple<int,int>(a.KeyCount(),) }).AsEnumerable().Select(c => new Tuple<string, int>(c.name.ToString(), c.count)).ToList();
        }
        /*public async Task<ActionResult<Dictionary<string, int>>> ProductsInventory()
        {
            return _context.Products.GroupBy(p => p.Title).Select(a => new { name = a.Key, count = a.Count() }).ToDictionary(g => g.name.ToString(), g => g.count);
        }*/
        }
    }
}