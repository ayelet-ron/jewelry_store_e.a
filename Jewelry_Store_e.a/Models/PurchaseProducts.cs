using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Jewelry_Store_e.a.Models
{
    public class PurchaseProducts
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Order")]
        public int OrderID { get; set; }
        [ForeignKey("Product")]
        public int ProductID { get; set; }
        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
