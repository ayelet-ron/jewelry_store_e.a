using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Jewelry_Store_e.a.Models
{
    public enum color
    {
        Gold,Silver
    };
    public enum title
    {
        Ring,Necklace, Women_Bracelet, Men_Bracelet, Earrings
    };
    public class Product
    {
        public int ID { get; set; }
        public title Title { get; set; }
        [StringLength(50, MinimumLength = 2)]
        [Required]
        public string Name { get; set; }
        [Range(10, 1000)]
        public double price { get; set; }
        public color Color { get; set; }
        [Required]
        public string Image { get; set; }
        public bool Sale { get; set; }

    }
}
