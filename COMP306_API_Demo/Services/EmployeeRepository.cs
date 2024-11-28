using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Runtime;
using Microsoft.Extensions.Configuration;
using ProductLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COMP306_API_Demo.Services
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DynamoDBContext _context;
        private readonly AmazonDynamoDBClient _dynamoDbClient;

        public EmployeeRepository(IConfiguration configuration)
        {
            var credentials = new BasicAWSCredentials(
                configuration["AWS:AccessKey"],
                configuration["AWS:SecretKey"]
            );
            var region = RegionEndpoint.USEast2;
            _dynamoDbClient = new AmazonDynamoDBClient(credentials, region);
            _context = new DynamoDBContext(_dynamoDbClient);
        }

        public async Task AddEmployeeAsync(Employee employee)
        {
            await _context.SaveAsync(employee);
        }

        public async Task DeleteEmployeeAsync(long id)
        {
            await _context.DeleteAsync<Employee>(id);
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            try
            {
                var employees = await _context.ScanAsync<Employee>(new List<ScanCondition>()).GetRemainingAsync();
                return employees;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }

        public async Task<Employee> GetEmployeeAsync(long id)
        {
            return await _context.LoadAsync<Employee>(id);
        }

        public async Task UpdateEmployeeAsync(Employee employee)
        {
            await _context.SaveAsync(employee);
        }
    }
}
