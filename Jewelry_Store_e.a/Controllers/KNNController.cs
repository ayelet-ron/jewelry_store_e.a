using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.IO;
using Jewelry_Store_e.a.Models;
using System.Security.Claims;

namespace Jewelry_Store_e.a.Controllers
{
    public class KNNController : BaseController
    {
        public KNNController(SDMDbContext context) : base(context)
        {
        }
        public async IActionResult Index()
        {
            List<Product> products = _context.Products.ToList();
            List<Product> recommendedProducts = new List<Product>();
            List<Product> purchaseProducts = new List<Product>();
            int customerId = int.Parse(User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Sid).Value);
            var recentOrders = _context.Orders.Where(order => order.CustomerID == customerId).AsQueryable();
            if (recentOrders.ToList().Count > 3) {
                recentOrders = recentOrders.OrderByDescending(a => a.OrderDate).Take(3);
            }
            foreach (var order in recentOrders)
            {
                foreach(var purchase in order.PurchaseProducts)
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
            List <int> recommanded = Knn.Distance(vectors, products);
            foreach (int id in recommanded)
            {
                recommendedProducts.Add((Product)products.Where(a => a.ID == id));
            }

            return View(await recommendedProducts);
        }
    }
}
        