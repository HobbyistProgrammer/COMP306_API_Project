using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COMP306_API_Demo.Models
{
    public class ProductCreateUpdateDto
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string Supplier { get; set; }
        public string Category { get; set; }
        public int SKU { get; set; }
        public int QuantityLeft { get; set; }
        public string ProductUnit { get; set; }
    }
}
