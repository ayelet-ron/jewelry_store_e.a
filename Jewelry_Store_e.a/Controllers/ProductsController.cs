using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Jewelry_Store_e.a.Data;
using Jewelry_Store_e.a.Models;
using Microsoft.AspNetCore.Authorization;

namespace Jewelry_Store_e.a.Controllers
{
    [Authorize]
    public class ProductsController : BaseController
    {
        public ProductsController(SDMDbContext context) : base(context)
        {
        }
        [AllowAnonymous]
        public async Task<IActionResult> Home()
        {
            return View();
        }
        // GET: Products
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
            if (searchprice>0)
            {
                products = products.Where(s => (int)s.price<=(searchprice));
            }
            return View(await products.ToListAsync());
        }
        /*public async Task<IActionResult> Index()
        {
            return View(await _context.Products.ToListAsync());
        }*/
        public async Task<IActionResult> Rings()
        {
            var rings = from p in _context.Products
                           select p;
            rings = rings.Where(s => s.Title.Equals(title.Ring));
            return View(await rings.ToListAsync());
        }
        public async Task<IActionResult> Sale()
        {
            var sale = from p in _context.Products
                        select p;
            sale = sale.Where(s => s.Sale.Equals(true));
            return View(await sale.ToListAsync());
        }
        public async Task<IActionResult> Necklace()
        {
            var rings = from p in _context.Products
                        select p;
            rings = rings.Where(s => s.Title.Equals(title.Necklace));
            return View(await rings.ToListAsync());
        }
        public async Task<IActionResult> Women_Bracelet()
        {
            var rings = from p in _context.Products
                        select p;
            rings = rings.Where(s => s.Title.Equals(title.Women_Bracelet));
            return View(await rings.ToListAsync());
        }
        public async Task<IActionResult> Men_Bracelet()
        {
            var rings = from p in _context.Products
                        select p;
            rings = rings.Where(s => s.Title.Equals(title.Men_Bracelet));
            return View(await rings.ToListAsync());
        }
        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.ID == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("ID,Title,Name,price,Color,Image,Sale")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title,Name,price,Color,Image,Sale")] Product product)
        {
            if (id != product.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ID))
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
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.ID == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ID == id);
        }
    }
}
