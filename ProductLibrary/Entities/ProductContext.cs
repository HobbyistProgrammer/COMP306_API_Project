using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Runtime;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductLibrary.Entities
{
    public partial class ProductContext
    {
        private readonly DynamoDBContext _context;

        public virtual DbSet<Product> Products { get; set; }

        public ProductContext(IConfiguration configuration)
        {
            // Fetch AWS credentials and region from configuration
            var credentials = new BasicAWSCredentials(
                configuration["AWS:AccessKey"],
                configuration["AWS:SecretKey"]
            );
            var region = Amazon.RegionEndpoint.USEast2;

            // Initialize DynamoDB client
            var dynamoDbClient = new AmazonDynamoDBClient(credentials, region);

            // Initialize DynamoDBContext
            _context = new DynamoDBContext(dynamoDbClient);
        }
        // Retrieve a product by Id
        public async Task<Product> GetProductAsync(long id)
        {
            return await _context.LoadAsync<Product>(id);
        }

        // Retrieve all products
        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _context.ScanAsync<Product>(new List<ScanCondition>()).GetRemainingAsync();
        }

        // Add a new product
        public async Task AddProductAsync(Product product)
        {
            await _context.SaveAsync(product);
        }

        // Update an existing product
        public async Task UpdateProductAsync(Product product)
        {
            await _context.SaveAsync(product);
        }

        // Delete a product by Id
        public async Task DeleteProductAsync(long id)
        {
            await _context.DeleteAsync<Product>(id);
        }
    }
}