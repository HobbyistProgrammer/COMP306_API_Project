using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductLibrary.Entities
{
    [DynamoDBTable("Products")]
    public class Product
    {
        [DynamoDBHashKey]
        public long Id { get; set; }
        [DynamoDBProperty]
        public string Name { get; set; }
        [DynamoDBProperty]
        public double Price { get; set; }
        [DynamoDBProperty]
        public string Description { get; set; }
        [DynamoDBProperty]
        public string Supplier { get; set; }
        [DynamoDBProperty]
        public string Category { get; set; }
        [DynamoDBProperty]
        public int SKU { get; set; }
        [DynamoDBProperty]
        public int QuantityLeft { get; set; }
        [DynamoDBProperty]
        public string ProductUnit { get; set; }
    }
}
