﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COMP306_API_Demo.Models
{
    public class ProductSummaryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public int SKU { get; set; }
        public string Category { get; set; }
        public int QuantityLeft { get; set; }

        public string ToString()
        {
            return ("Id: " + Id + " - Name: " + Name + " - SKU: " + SKU +
                "\nPrice: $" + Price +
                "\nDescription: " + Description +
                "\nCategory: " + Category +
                "\nQuantity Left: " + QuantityLeft + "\n");
        }
    }
}
