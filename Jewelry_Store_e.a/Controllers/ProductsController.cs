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
using System.Security.Claims;

namespace Jewelry_Store_e.a.Controllers
{
    [Authorize]
    public class ProductsController : BaseController
    {
        public ProductsController(SDMDbContext context) : base(context)
        {
        }
        [AllowAnonymous]
        public IActionResult Home(string searchString, string searchcolor, int searchprice)
        {
            Tuple<List<Product>, bool> filter = Filter(searchString, searchcolor, searchprice);
            if (filter.Item2)
            {
                if (filter.Item1.Count == 0)
                {
                    return RedirectToAction("NoIndexFound");   
                }
                return View(filter.Item1);
            }
            List<Product> recommanded = KnnRecommandation();
            if (recommanded.Count == 0)
            {
                recommanded = _context.Products.ToList();
                return View(new Tuple<List<Product>, bool>(recommanded, false));
            }
            return View(new Tuple<List<Product>, bool>(recommanded, true));
        }
        public IActionResult NoIndexFound()
        {
            return View();
        }
        // GET: Products
        public Tuple<List<Product>,bool> Filter(string searchString, string searchcolor, int searchprice)
        {
            bool search = false;
            var products = from p in _context.Products
                           select p;
            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(s => s.Name.Contains(searchString));
                search = true;
            }
            if (!String.IsNullOrEmpty(searchcolor))
            {
                search = true;
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
                search = true;
                products = products.Where(s => (int)s.price<=(searchprice));
            }
            return new Tuple<List<Product>,bool>(products.ToList(), search);
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Products.ToListAsync());
        }
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
            var necklace = from p in _context.Products
                        select p;
            necklace = necklace.Where(s => s.Title.Equals(title.Necklace));
            return View(await necklace.ToListAsync());
        }
        public async Task<IActionResult> Women_Bracelet()
        {
            var wBracelet = from p in _context.Products
                        select p;
            wBracelet = wBracelet.Where(s => s.Title.Equals(title.Women_Bracelet));
            return View(await wBracelet.ToListAsync());
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
        [Authorize(Roles = "Admin")]
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
        public List<Product> KnnRecommandation()
        {
            List<Product> products = _context.Products.ToList();
            List<Product> recommendedProducts = new List<Product>();
            List<Product> purchaseProducts = new List<Product>();
            if (User.Identity.IsAuthenticated)
            {
                int customerId = int.Parse(User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Sid).Value);
                var recentOrders = _context.Orders.Where(order => order.CustomerID == customerId).Include(o => o.customer).Include(o => o.PurchaseProducts).ThenInclude(p => p.Product).AsQueryable();
                if (recentOrders.ToList().Count > 3)
                {
                    recentOrders = recentOrders.OrderByDescending(a => a.OrderDate).Take(3);
                }
                foreach (var order in recentOrders)
                {
                    foreach (var purchase in order.PurchaseProducts)
                    {
                        purchaseProducts.Add(purchase.Product);
                    }
                }
                int count = purchaseProducts.Count();
                int[,] vectors = new int[count, 3];
                for (int i = 0; i < count; i++)
                {
                    vectors[i, 0] = (int)purchaseProducts[i].Title;
                }
                for (int i = 0; i < count; i++)
                {
                    vectors[i, 1] = (int)purchaseProducts[i].Color;
                }
                for (int i = 0; i < count; i++)
                {
                    vectors[i, 2] = (int)purchaseProducts[i].price;
                }
                products.RemoveAll(p => purchaseProducts.Where(p2 => p.ID == p2.ID).ToList().Count != 0);//??????
                List<int> recommanded = Knn.Distance(vectors, products);
                foreach (int id in recommanded)
                {
                    recommendedProducts.Add(products.Where(a => a.ID == id).Single());
                }
            }
            return recommendedProducts;
        }
    }
}
