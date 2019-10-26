using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Jewelry_Store_e.a.Models;
using Microsoft.AspNetCore.Authorization;

namespace Jewelry_Store_e.a.Controllers
{
    [Authorize]
    public class PurchaseProductsController : BaseController
    {
        public PurchaseProductsController(JewelryContext context) :base(context)
        {
        }

        // GET: PurchaseProducts
        public async Task<IActionResult> Index()
        {
            var jewelry = _context.PurchaseProducts.Include(p => p.Order).Include(p => p.Product);
            return View(await jewelry.ToListAsync());
        }

        // GET: PurchaseProducts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseProduct = await _context.PurchaseProducts
                .Include(p => p.Order)
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (purchaseProduct == null)
            {
                return NotFound();
            }

            return View(purchaseProduct);
        }

        // GET: PurchaseProducts/Create
        public IActionResult Create()
        {
            ViewData["OrderID"] = new SelectList(_context.Orders, "Id", "Id");
            ViewData["ProductID"] = new SelectList(_context.Products, "ID", "Image");
            return View();
        }

        // POST: PurchaseProducts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,OrderID,ProductID")] PurchaseProduct purchaseProduct)
        {
            if (ModelState.IsValid)
            {
                _context.Add(purchaseProduct);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderID"] = new SelectList(_context.Orders, "Id", "Id", purchaseProduct.OrderID);
            ViewData["ProductID"] = new SelectList(_context.Products, "ID", "Image", purchaseProduct.ProductID);
            return View(purchaseProduct);
        }

        // GET: PurchaseProducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseProduct = await _context.PurchaseProducts.FindAsync(id);
            if (purchaseProduct == null)
            {
                return NotFound();
            }
            ViewData["OrderID"] = new SelectList(_context.Orders, "Id", "Id", purchaseProduct.OrderID);
            ViewData["ProductID"] = new SelectList(_context.Products, "ID", "Image", purchaseProduct.ProductID);
            return View(purchaseProduct);
        }

        // POST: PurchaseProducts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,OrderID,ProductID")] PurchaseProduct purchaseProduct)
        {
            if (id != purchaseProduct.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(purchaseProduct);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PurchaseProductExists(purchaseProduct.Id))
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
            ViewData["OrderID"] = new SelectList(_context.Orders, "Id", "Id", purchaseProduct.OrderID);
            ViewData["ProductID"] = new SelectList(_context.Products, "ID", "Image", purchaseProduct.ProductID);
            return View(purchaseProduct);
        }

        // GET: PurchaseProducts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseProduct = await _context.PurchaseProducts
                .Include(p => p.Order)
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (purchaseProduct == null)
            {
                return NotFound();
            }

            return View(purchaseProduct);
        }

        // POST: PurchaseProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var purchaseProduct = await _context.PurchaseProducts.FindAsync(id);
            _context.PurchaseProducts.Remove(purchaseProduct);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PurchaseProductExists(int id)
        {
            return _context.PurchaseProducts.Any(e => e.Id == id);
        }
    }
}
