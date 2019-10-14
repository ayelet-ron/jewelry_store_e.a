using Jewelry_Store_e.a.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Jewelry_Store_e.a.Data
{
    public static class DbInitializer
    {
        public static void Initialize(SDMDbContext context)
        {
            context.Database.EnsureCreated();

            // Look for any customers.
            /* if (context.Customers.Any())
             {
                 return;   // DB has been seeded
             }
            Customer c = new Customer
            {
                FirstName = "Admin",
                LastName = "Admin",
                BirthDate = new DateTime(1995, 11, 04),
                Email = "admin@gmail.com",
                Password = "admin",
                IsAdmin = true
            };
            context.Customers.Add(c);
            context.SaveChanges();
            Product p = new Product
            {
                Name = "errings",
                Title = title.Earrings,
                Image = "~/images/Eye necklace.jpg",
                Sale = true,
                Color = color.Gold,
                price = 100
            };
            context.Products.Add(p);
            context.SaveChanges();
            Order o = new Order
            {
                CustomerID = c.ID,
                OrderDate = DateTime.Now
            };
            context.Orders.Add(o);
            context.SaveChanges();
            PurchaseProduct s = new PurchaseProduct
            {
                OrderID=o.Id,
                ProductID=p.ID
            };
            context.PurchaseProducts.Add(s);
            context.SaveChanges();
            PurchaseProduct s1 = new PurchaseProduct
            {
                OrderID = 1,
                ProductID = 1
            };
            context.PurchaseProducts.Add(s1);
            context.SaveChanges();
            Product p = new Product
            {
                Name = "seshell",
                Title = title.Women_Bracelet,
                Image = "Seashell bracelet.jpg",
                Sale = false,
                Color = color.Silver,
                price = 70
            };
            context.Products.Add(p);
            context.SaveChanges();
            Product p1 = new Product
            {
                Name = "Big Eye",
                Title = title.Ring,
                Image = "Eye ring.jpg",
                Sale = false,
                Color = color.Silver,
                price = 85
            };
            context.Products.Add(p1);
            context.SaveChanges();
            context.Shipments.AddRange(new List<Shipment>() {
                new Shipment
                {
                    Address="Raoul Wallenberg 24",
                    City = "tel aviv"
                }
            });
            context.Products.AddRange(new List<Product>() {
                new Product
                {
                    Name = "set of braclet",
                    Title = title.Men_Bracelet,
                    Image = "set bracelet.jpg",
                    Sale = false,
                    Color = color.Gold,
                    price = 60
                },
                new Product
                {
                    Name = "blue set bracelet",
                    Title = title.Men_Bracelet,
                    Image = "blue set bracelet.jpg",
                    Sale = false,
                    Color = color.Silver,
                    price = 80
                },
                new Product
                {
                    Name = "bracelet ring",
                    Title = title.Women_Bracelet,
                    Image = "bracelet ring.jpg",
                    Sale = false,
                    Color = color.Gold,
                    price = 140
                },
                new Product
                {
                    Name = "Big Eye",
                    Title = title.Ring,
                    Image = "Eye ring.jpg",
                    Sale = false,
                    Color = color.Silver,
                    price = 85
                },
                new Product
                {
                    Name = "Green stone necklace",
                    Title = title.Necklace,
                    Image = "Green stone necklace.jpg",
                    Sale = false,
                    Color = color.Silver,
                    price = 90
                },
                new Product
                {
                    Name = "tree eye ring",
                    Title = title.Earrings,
                    Image = "tree eye ring.jpg",
                    Sale = false,
                    Color = color.Silver,
                    price = 85
                }
            });
            context.SaveChanges();
            */
        }

    }
}
