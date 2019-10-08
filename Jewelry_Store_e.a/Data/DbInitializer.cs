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
            }*/

            context.Customers.AddRange(
                    new List<Customer>() {
                        new Customer
                        {
                            //Id = 1,
                            FirstName = "Admin",
                            LastName = "Admin",
                            BirthDate = new DateTime(1995, 11, 04),
                            Email = "admin@sdm.com",
                            Password = "admin",
                            IsAdmin = true
                        }
                        });
            context.Products.AddRange(new List<Product>() {
                        new Product
                        {
                           // Id = 1,
                            Name = "Milk 3% fat",
                            Title = title.Earrings,
                            Image="~/images/Eye necklace.jpg",
                            Sale = true,
                            Color=color.Gold,
                            price = 100
                        }
            }
            );
            context.Orders.AddRange(
                    new List<Order>() {
                        new Order
                        {
                            CustomerID = 1,
                            ProductID = {1 },
                            OrderDate = DateTime.Now.AddDays(-1).AddHours(-2),
                            Rate=3
                        }
                    }
                    );
        }
    }
}
