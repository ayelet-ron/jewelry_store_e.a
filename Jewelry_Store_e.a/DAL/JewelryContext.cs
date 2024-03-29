﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Web;

namespace Jewelry_Store_e.a.Models
{
    public class JewelryContext : DbContext
    {
        #region Properties
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<PurchaseProduct> PurchaseProducts { get; set; }
        #endregion

        public JewelryContext(DbContextOptions<JewelryContext> options) : base(options)
        {

        }

        public JewelryContext() : base()
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Customer>(entity => {
                entity.HasIndex(e => e.Email).IsUnique();
            });
        }
    }
}

