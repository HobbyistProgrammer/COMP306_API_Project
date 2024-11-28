using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Runtime;
using Microsoft.Extensions.Configuration;
using ProductLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace COMP306_API_Demo.Services
{
    public class ProductRepository : IProductRepository
    {
        private readonly DynamoDBContext _context;
        private readonly AmazonDynamoDBClient _dynamoDbClient;

        public ProductRepository(IConfiguration configuration)
        {
            var credentials = new BasicAWSCredentials(
                configuration["AWS:AccessKey"],
                configuration["AWS:SecretKey"]
            );
            var region = RegionEndpoint.USEast2;
            _dynamoDbClient = new AmazonDynamoDBClient(credentials, region);
            _context = new DynamoDBContext(_dynamoDbClient);
        }

        public async Task AddProductAsync(Product product)
        {
            await _context.SaveAsync(product);
        }

        public async Task DeleteProductAsync(long id)
        {
            await _context.DeleteAsync<Product>(id);
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            try
            {
                var products = await _context.ScanAsync<Product>(new List<ScanCondition>()).GetRemainingAsync();
                return products;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }

        public async Task<Product> GetProductAsync(long id)
        {
            return await _context.LoadAsync<Product>(id);
        }

        public async Task UpdateProductAsync(Product product)
        {
            await _context.SaveAsync(product);
        }
    }
}
