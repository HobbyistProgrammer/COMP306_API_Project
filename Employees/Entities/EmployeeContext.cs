using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Runtime;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeLibrary.Entities
{
    class EmployeeContext
    {
        private readonly DynamoDBContext _context;

        public virtual DbSet<Employee> Employees { get; set; }

        public EmployeeContext(IConfiguration configuration)
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
        public async Task<Employee> GetEmployeeAsync(long id)
        {
            return await _context.LoadAsync<Employee>(id);
        }

        // Retrieve all products
        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            return await _context.ScanAsync<Employee>(new List<ScanCondition>()).GetRemainingAsync();
        }

        // Add a new product
        public async Task AddEmployeeAsync(Employee employee)
        {
            await _context.SaveAsync(employee);
        }

        // Update an existing product
        public async Task UpdateEmployeeAsync(Employee employee)
        {
            await _context.SaveAsync(employee);
        }

        // Delete a product by Id
        public async Task DeleteEmployeeAsync(long id)
        {
            await _context.DeleteAsync<Employee>(id);
        }
    }
}
