using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProductLibrary.Entities
{
    [DynamoDBTable("Products")]
    public class Product
    {
        [DynamoDBHashKey]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; } 
        [DynamoDBProperty("Category")]
        public string Category { get; set; }
        [DynamoDBProperty]
        public string Description { get; set; }
        [DynamoDBProperty]
        public string Name { get; set; }
        [DynamoDBProperty]
        public double Price { get; set; }
        [DynamoDBProperty("ProductUnit")]
        public string ProductUnit { get; set; }
        [DynamoDBProperty]
        public int QuantityLeft { get; set; }
        [DynamoDBProperty]
        public int SKU { get; set; }
        [DynamoDBProperty("Supplier")]
        public string Supplier { get; set; }
    }
}
