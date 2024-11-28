using ProductLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COMP306_API_Demo.Services
{
    public interface IEmployeeRepository
    {
        Task<Employee> GetEmployeeAsync(long id);
        // Retrieve all products
        Task<IEnumerable<Employee>> GetAllEmployeesAsync();
        // Add a new product
        Task AddEmployeeAsync(Employee employee);
        // Update an existing product
        Task UpdateEmployeeAsync(Employee employee);
        // Delete a product by Id
        Task DeleteEmployeeAsync(long id);
    }
}
