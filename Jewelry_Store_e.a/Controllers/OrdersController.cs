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
        public OrdersController(SDMDbContext context) : base(context)
        {
        }
        [AllowAnonymous]
        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var SDMDbContext = _context.Orders.Include(o => o.customer).Include(o => o.products);
            return View(await SDMDbContext.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.customer)
                .Include(o => o.products)
                .FirstOrDefaultAsync(m => m.OrderID == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }
        public async Task<IActionResult> Cart()
        {
            var order = from p in _context.Orders.Include(o => o.customer).Include(o => o.products)
                        select p;
            order = order.Where(o => o.OrderDate.Equals(DateTime.Today.Date));
            if (User.Identity.IsAuthenticated)
            {
                int customerId = int.Parse(User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Sid).Value);
                try
                {
                    order = order.Where(o => o.CustomerID == customerId);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            if (order == null)
            {
                return View(await order.ToListAsync());
            }

            return View(await order.ToListAsync());
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["ID"] = new SelectList(_context.Customers, "ID", "ID");
            ViewData["ProductID"] = new SelectList(_context.Products, "ID", "Name");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderID,CustomerID,ProductID,Rate,OrderDate")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ID"] = new SelectList(_context.Customers, "ID", "ID", order.CustomerID);
            ViewData["ProductID"] = new SelectList(_context.Products, "ID", "Name", order.ProductID);
            return View(order);
        }

        // GET: Orders/Edit/5
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
            ViewData["ProductID"] = new SelectList(_context.Products, "ID", "Name", order.ProductID);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderID,CustomerID,ProductID,Rate,OrderDate")] Order order)
        {
            if (id != order.OrderID)
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
                    if (!OrderExists(order.OrderID))
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
            ViewData["ProductID"] = new SelectList(_context.Products, "ID", "Name", order.ProductID);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.customer)
                .Include(o => o.products)
                .FirstOrDefaultAsync(m => m.OrderID == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.OrderID == id);
        }
    }
}
