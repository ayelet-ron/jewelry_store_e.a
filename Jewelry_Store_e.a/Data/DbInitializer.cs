using Jewelry_Store_e.a.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Jewelry_Store_e.a.Data
{
    public static class DbInitializer
    {
        public static void Initialize(JewelryContext context)
        {
            context.Database.EnsureCreated();

            // Look for any customers.
            /*if (context.Customers.Any())
             {
                 return;   // DB has been seeded
             }*/
            Customer c = new Customer
            {
                FirstName = "Chen",
                LastName = "Amiel",
                BirthDate = new DateTime(1995, 11, 04),
                Email = "Chen@gmail.com",
                Password = "admin",
                IsAdmin = true
            };
            context.Customers.Add(c);
            context.SaveChanges();
            Order o = new Order
            {
                CustomerID = c.ID,
                OrderDate = DateTime.Today.AddDays(-3),
                Purchase = true
            };
            context.Orders.Add(o);
            context.SaveChanges();
            PurchaseProduct s = new PurchaseProduct
            {
                OrderID=o.Id,
                ProductID=1022
            };
            context.PurchaseProducts.Add(s);
            context.SaveChanges();
            ////////////////////////////
            Order o1 = new Order
            {
                CustomerID = c.ID,
                OrderDate = DateTime.Today.AddDays(-5),
                Purchase=true
            };
            context.Orders.Add(o1);
            context.SaveChanges();
            PurchaseProduct s2 = new PurchaseProduct
            {
                OrderID = o1.Id,
                ProductID = 1024
            };
            context.PurchaseProducts.Add(s2);
            context.SaveChanges();
            PurchaseProduct s3 = new PurchaseProduct
            {
                OrderID = o1.Id,
                ProductID = 1025
            };
            context.PurchaseProducts.Add(s3);
            context.SaveChanges();
            /////////////////////////
            Order o2 = new Order
            {
                CustomerID = c.ID,
                OrderDate = DateTime.Today.AddDays(-8),
                Purchase = true
            };
            context.Orders.Add(o2);
            context.SaveChanges();
            PurchaseProduct s4 = new PurchaseProduct
            {
                OrderID = o2.Id,
                ProductID = 1035
            };
            context.PurchaseProducts.Add(s4);
            context.SaveChanges();
            //////////////////////////////////////////////////////////
            Order o3 = new Order
            {
                CustomerID = 7,
                OrderDate = DateTime.Today.AddDays(-6),
                Purchase = true
            };
            context.Orders.Add(o3);
            context.SaveChanges();
            PurchaseProduct s5 = new PurchaseProduct
            {
                OrderID = o3.Id,
                ProductID = 1028
            };
            context.PurchaseProducts.Add(s5);
            context.SaveChanges();
            PurchaseProduct s6 = new PurchaseProduct
            {
                OrderID = o3.Id,
                ProductID = 1029
            };
            context.PurchaseProducts.Add(s6);
            context.SaveChanges();
            /////////////////////////////////////////////////
            Order o4 = new Order
            {
                CustomerID = 7,
                OrderDate = DateTime.Today.AddDays(-12),
                Purchase = true
            };
            context.Orders.Add(o4);
            context.SaveChanges();
            PurchaseProduct s8 = new PurchaseProduct
            {
                OrderID = o4.Id,
                ProductID = 1033
            };
            context.PurchaseProducts.Add(s8);
            context.SaveChanges();
            PurchaseProduct s13 = new PurchaseProduct
            {
                OrderID = o4.Id,
                ProductID = 1013
            };
            context.PurchaseProducts.Add(s13);
            context.SaveChanges();
            /////////////////////////////////////
            Order o5 = new Order
            {
                CustomerID = 7,
                OrderDate = DateTime.Today.AddDays(-16),
                Purchase = true
            };
            context.Orders.Add(o5);
            context.SaveChanges();
            PurchaseProduct s9 = new PurchaseProduct
            {
                OrderID = o5.Id,
                ProductID = 1013
            };
            context.PurchaseProducts.Add(s9);
            context.SaveChanges();
            PurchaseProduct s11 = new PurchaseProduct
            {
                OrderID = o5.Id,
                ProductID = 1015
            };
            context.PurchaseProducts.Add(s11);
            context.SaveChanges();
            /*context.Shipments.AddRange(new List<Shipment>() {
                new Shipment
                {
                    Address="Raoul Wallenberg 24, tel aviv",
                },
                new Shipment
                {
                    Address="Ha-Mano'a St 7, Tel Aviv-Yafo",
                },
                new Shipment
                {
                    Address="Ha-Lokhamim St 62, Holon",
                },
                new Shipment
                {
                    Address="Yigal Alon St 120, Tel Aviv-Yafo",
                },
                new Shipment
                {
                    Address="yamit 62, rishon le ziyon",
                }
            });
            context.SaveChanges();
            context.Products.AddRange(new List<Product>() {
                new Product
                {
                    Name = "circle ring",
                    Title = title.Ring,
                    Image = "circle ring.JPG",
                    Sale = false,
                    Color = color.Silver,
                    price =28
                },
                new Product
                {
                    Name = "charms braclet",
                    Title = title.Women_Bracelet,
                    Image = "charms braclet.JPG",
                    Sale = false,
                    Color = color.Silver,
                    price =25
                },
                new Product
                {
                    Name = "eye errings",
                    Title = title.Earrings,
                    Image = "eye errings.JPG",
                    Sale = false,
                    Color = color.Gold,
                    price =25
                },
                new Product
                {
                    Name = "‏‏bracelet ring",
                    Title = title.Women_Bracelet,
                    Image = "‏‏bracelet ring colors.jpg",
                    Sale = false,
                    Color = color.Silver,
                    price =40
                },
                new Product
                {
                    Name = "xxx ring",
                    Title = title.Ring,
                    Image = "xxx ring.JPG",
                    Sale = false,
                    Color = color.Silver,
                    price =28
                },
                new Product
                {
                    Name = "3 starts",
                    Title = title.Necklace,
                    Image = "3 starts necklace.jpg",
                    Sale = false,
                    Color = color.Silver,
                    price = 38
                },
                new Product
                {
                    Name = "Horseshoe",
                    Title = title.Women_Bracelet,
                    Image = "Horseshoe bracelet.jpg",
                    Sale = false,
                    Color = color.Gold,
                    price = 26
                },
                new Product
                {
                    Name = "loop errings",
                    Title = title.Earrings,
                    Image = "loop errings.JPG",
                    Sale = false,
                    Color = color.Gold,
                    price =29
                },
                new Product
                {
                    Name = "small eye",
                    Title = title.Necklace,
                    Image = "Eye necklace.jpg",
                    Sale = false,
                    Color = color.Silver,
                    price =27
                },
                new Product
                {
                    Name = "v necklace",
                    Title = title.Necklace,
                    Image = "v necklace.jpg",
                    Sale = false,
                    Color = color.Silver,
                    price =34
                },
                new Product
                {
                    Name = "stars errings",
                    Title = title.Earrings,
                    Image = "stars errings.JPG",
                    Sale = false,
                    Color = color.Gold,
                    price =26
                },
                new Product
                {
                    Name = "the big eye",
                    Title = title.Women_Bracelet,
                    Image = "eye braclet.JPG",
                    Sale = false,
                    Color = color.Gold,
                    price =30
                },
                new Product
                {
                    Name = "Eye ring",
                    Title = title.Ring,
                    Image = "Eye ring.jpg",
                    Sale = false,
                    Color = color.Silver,
                    price =34
                },
                new Product
                {
                    Name = "men bracleate",
                    Title = title.Men_Bracelet,
                    Image = "men bracleate.JPG",
                    Sale = false,
                    Color = color.Gold,
                    price =18
                },
                new Product
                {
                    Name = "seashell long",
                    Title = title.Necklace,
                    Image = "seashell long nacklace.JPG",
                    Sale = false,
                    Color = color.Silver,
                    price =30
                },
                new Product
                {
                    Name = "set of errings",
                    Title = title.Earrings,
                    Image = "set of errings.JPG",
                    Sale = false,
                    Color = color.Gold,
                    price =26
                },
                new Product
                {
                    Name = "long errings",
                    Title = title.Earrings,
                    Image = "long errings.JPG",
                    Sale = false,
                    Color = color.Gold,
                    price =24
                },
                new Product
                {
                    Name = "party set braclet",
                    Title = title.Men_Bracelet,
                    Image = "set bracelet.jpg",
                    Sale = false,
                    Color = color.Silver,
                    price = 20
                },
                new Product
                {
                    Name = "blue set bracelet",
                    Title = title.Men_Bracelet,
                    Image = "blue set bracelet.jpg",
                    Sale = false,
                    Color = color.Silver,
                    price = 20
                },
                new Product
                {
                    Name = "long seashell",
                    Title = title.Necklace,
                    Image = "necklace long seashell.JPG",
                    Sale = false,
                    Color = color.Gold,
                    price =32
                },
                new Product
                {
                    Name = "‏‏Ring Colors",
                    Title = title.Ring,
                    Image = "‏‏RingColors.jpg",
                    Sale = false,
                    Color = color.Silver,
                    price =19
                },
                new Product
                {
                    Name = "red snake",
                    Title = title.Ring,
                    Image = "red snake ring.JPG",
                    Sale = false,
                    Color = color.Silver,
                    price =26
                },
                new Product
                {
                    Name = "big star",
                    Title = title.Necklace,
                    Image = "‏‏big star necklace.JPG",
                    Sale = false,
                    Color = color.Silver,
                    price = 30
                },
                new Product
                {
                    Name = "charms necklace",
                    Title = title.Necklace,
                    Image = "charms necklace.JPG",
                    Sale = false,
                    Color = color.Silver,
                    price =32
                },
                new Product
                {
                    Name = "just gold",
                    Title = title.Men_Bracelet,
                    Image = "clean braclet.JPG",
                    Sale = false,
                    Color = color.Gold,
                    price =20
                },
                new Product
                {
                    Name = "blue stone",
                    Title = title.Earrings,
                    Image = "blue stone errings.JPG",
                    Sale = true,
                    Color = color.Silver,
                    price = 26
                },
                new Product
                {
                    Name = "blue eye",
                    Title = title.Ring,
                    Image = "blue eye ring.JPG",
                    Sale = true,
                    Color = color.Silver,
                    price =19
                },
                new Product
                {
                    Name = "big circle",
                    Title = title.Women_Bracelet,
                    Image = "big circle braclate.JPG",
                    Sale = true,
                    Color = color.Silver,
                    price =25
                },
                new Product
                {
                    Name = "dimond",
                    Title = title.Ring,
                    Image = "dimond ring.JPG",
                    Sale = true,
                    Color = color.Silver,
                    price =35
                },
                new Product
                {
                    Name = "fish hurt drop",
                    Title = title.Women_Bracelet,
                    Image = "fish hurt drop.JPG",
                    Sale = true,
                    Color = color.Gold,
                    price =19
                },
                new Product
                {
                    Name = "‏‏fish",
                    Title = title.Ring,
                    Image = "‏‏fish ring.JPG",
                    Sale = true,
                    Color = color.Silver,
                    price =16
                },
                new Product
                {
                    Name = "hoop",
                    Title = title.Earrings,
                    Image = "hoop errings.JPG",
                    Sale = false,
                    Color = color.Silver,
                    price =22
                },
                new Product
                {
                    Name = "snake ring",
                    Title = title.Ring,
                    Image = "snake ring.JPG",
                    Sale = false,
                    Color = color.Silver,
                    price =26
                },
                new Product
                {
                    Name = "stars and moon",
                    Title = title.Necklace,
                    Image = "stars and moon necklace.jpg",
                    Sale = false,
                    Color = color.Silver,
                    price =33
                },
                new Product
                {
                    Name = "‏‏drop",
                    Title = title.Necklace,
                    Image = "‏‏drop necklace.JPG",
                    Sale = true,
                    Color = color.Silver,
                    price =10
                },
                new Product
                {
                    Name = "eye braclete.JPG",
                    Title = title.Women_Bracelet,
                    Image = "eye braclete.JPG",
                    Sale = true,
                    Color = color.Silver,
                    price =17
                },
                new Product
                {
                    Name = "bug leave",
                    Title = title.Earrings,
                    Image = "bug leave errings.JPG",
                    Sale = true,
                    Color = color.Silver,
                    price =24
                },
                new Product
                {
                    Name = "Green stone necklace",
                    Title = title.Necklace,
                    Image = "Green stone necklace.jpg",
                    Sale = true,
                    Color = color.Silver,
                    price = 18
                },
                new Product
                {
                    Name = "hurt",
                    Title = title.Earrings,
                    Image = "hurt errings.JPG",
                    Sale = true,
                    Color = color.Gold,
                    price =17
                },
                new Product
                {
                    Name = "v long",
                    Title = title.Necklace,
                    Image = "vlong.jpg",
                    Sale = false,
                    Color = color.Silver,
                    price =40
                },
                new Product
                {
                    Name = "long stars",
                    Title = title.Earrings,
                    Image = "long stars errings.JPG",
                    Sale = false,
                    Color = color.Silver,
                    price =20
                },
                new Product
                {
                    Name = "red eye",
                    Title = title.Women_Bracelet,
                    Image = "red eye braclet.JPG",
                    Sale = false,
                    Color = color.Gold,
                    price =23
                },
                new Product
                {
                    Name = "name necklace",
                    Title = title.Necklace,
                    Image = "name necklace.jpg",
                    Sale = true,
                    Color = color.Silver,
                    price =15
                },
                new Product
                {
                    Name = "shiny gold",
                    Title = title.Men_Bracelet,
                    Image = "shiny gold braclate.JPG",
                    Sale = false,
                    Color = color.Gold,
                    price =22
                },
                new Product
                {
                    Name = "tree eye ring",
                    Title = title.Ring,
                    Image = "tree eye ring.jpg",
                    Sale = false,
                    Color = color.Silver,
                    price =17
                },
                new Product
                {
                    Name = "stars ring",
                    Title = title.Ring,
                    Image = "stars ring.jpg",
                    Sale = false,
                    Color = color.Silver,
                    price =22
                },
                new Product
                {
                    Name = "Red stone",
                    Title = title.Necklace,
                    Image = "Red stone necklace.jpg",
                    Sale = false,
                    Color = color.Silver,
                    price =20
                }
            });
            context.SaveChanges();*/
        }
    }
}
