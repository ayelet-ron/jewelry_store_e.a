using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Jewelry_Store_e.a.Data;
using Jewelry_Store_e.a.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Jewelry_Store_e.a.Controllers
{
    [Authorize]
    public class OrdersController : BaseController
    {
        public OrdersController(JewelryContext context) : base(context)
        {
        }

        [Authorize(Roles = "Admin, Customer")]
        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var jewelry = _context.Orders.Include(o => o.customer).Include(o => o.PurchaseProducts);
            return View(await jewelry.ToListAsync());
        }
        public async Task<IActionResult> myOrders()
        {
            int customerId = int.Parse(User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Sid).Value);
            var jewelry = _context.Orders.Where(o=>o.CustomerID== customerId).Where(o=>o.Purchase==true).Include(o => o.customer).Include(o => o.PurchaseProducts);
            return View(await jewelry.ToListAsync());
        }
        [Authorize(Roles = "Admin")]
        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.customer)
                .Include(o => o.PurchaseProducts)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }
        public async Task<IActionResult> Cart()
        {

            var order = from p in _context.Orders.Include(o => o.customer).Include(o => o.PurchaseProducts).ThenInclude(p => p.Product)
                        select p;
            if (User.Identity.IsAuthenticated)
            {
                int customerId = int.Parse(User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Sid).Value);
                try
                {
                    Order current = order.Where(o => o.CustomerID == customerId).Where(a => a.Purchase == false).SingleOrDefault();
                    return View(current);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

            }
            return View(await order.ToListAsync());
        }
        public IActionResult AddToCart(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                int customerId = int.Parse(User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Sid).Value);
                //need to make sure that we have only one current order all the other from the same date are cloesed
                var currentOrder = _context.Orders.Where(o=>o.CustomerID==customerId).Where(a => a.Purchase == false).SingleOrDefault();
                if (currentOrder == null)
                {
                    Order newOrder = new Order
                    {
                        CustomerID = customerId,
                        OrderDate = DateTime.Now,
                        Purchase = false
                    };
                    _context.Orders.Add(newOrder);
                    _context.SaveChanges();
                    //int orderid = _context.Orders.Where(o => o.OrderDate == DateTime.Now.Date).Where(a => a.Purchase == false).Select(a => a.Id).Single();
                    _context.PurchaseProducts.Add(new PurchaseProduct { OrderID = newOrder.Id, ProductID = id });
                    _context.SaveChanges();
                    return RedirectToAction("Cart");

                }
                else
                {
                    _context.PurchaseProducts.Add(new PurchaseProduct { OrderID = currentOrder.Id, ProductID = id });
                    _context.SaveChanges();
                    return RedirectToAction("Cart");
                }
            }
            return View();
        }
        public IActionResult RemoveFromCart(int id)
        {
            PurchaseProduct p = _context.PurchaseProducts.Where(o => o.Id == id).Single();
            return RedirectToAction("Delete", "PurchaseProducts",new { id = id });
        }
        public IActionResult BuyNow(int id)
        { 
            if (User.Identity.IsAuthenticated)
            {
                Order order = _context.Orders.Where(o => o.Id == id).Single();
                string url = string.Format("/Orders/Edit?id={0}&id={1}&CustomerID={2}&OrderDate={3}&Purchase={4}", id, id, order.CustomerID, order.OrderDate, true);
                //this.Edit( id,[id, order.CustomerID, order.OrderDate, true]);
                order.Purchase = true;
                _context.Update(order);
                _context.SaveChanges();
            }
            return RedirectToAction("Home", "Products");
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["ID"] = new SelectList(_context.Customers, "ID", "ID");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Id,CustomerID,OrderDate,Purchase")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ID"] = new SelectList(_context.Customers, "ID", "ID", order.CustomerID);
            return View(order);
        }

        // GET: Orders/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["ID"] = new SelectList(_context.Customers, "ID", "ID", order.CustomerID);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CustomerID,OrderDate,Purchase")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ID"] = new SelectList(_context.Customers, "ID", "ID", order.CustomerID);
            return View(order);
        }

        // GET: Orders/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.customer)
                .Include(o => o.PurchaseProducts)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
