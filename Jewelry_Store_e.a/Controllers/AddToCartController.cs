using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Jewelry_Store_e.a.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Jewelry_Store_e.a.Controllers
{
    [Authorize]
    public class AddToCartController : BaseController
    {
        public AddToCartController(SDMDbContext context) : base(context)
        {
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Cart()
        {
            var order = from p in _context.Orders.Include(o => o.customer).Include(o => o.PurchaseProducts)
                        select p;
            order = order.Where(o => o.OrderDate.Equals(DateTime.Today.Date));
            if (User.Identity.IsAuthenticated)
            {
                int customerId = int.Parse(User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Sid).Value);
                try
                {
                    Order current = order.Where(o => o.CustomerID == customerId).Where(a => a.Purchase == false).Single();
                    List<Product> CartProducts = new List<Product>();
                    foreach(var b in current.PurchaseProducts)
                    {
                        CartProducts.Add(_context.Products.Where(p => p.ID == b.ProductID).Single());
                    }
                    return View(CartProducts);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

            }
            return View(await order.ToListAsync());
        }
    }
}