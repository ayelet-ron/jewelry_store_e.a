using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Jewelry_Store_e.a.Models
{
    //public enum rate int {1,2,3,4,5};
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public int CustomerID { get; set; }
        [ForeignKey("CustomerID")]
        public virtual Customer customer { get; set; }
        [Range(1, 5)]
        public int Rate { get; set; }
        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; }
        public ICollection<PurchaseProducts> PurchaseProducts { get; set; }
        public bool Purchase{ get; set; }
    }
}
