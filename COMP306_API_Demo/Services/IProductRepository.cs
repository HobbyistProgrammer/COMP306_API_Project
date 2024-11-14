using ProductLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COMP306_API_Demo.Services
{
    public interface IProductRepository
    {
        Task<Product> GetProductAsync(long id);
        // Retrieve all products
        Task<IEnumerable<Product>> GetAllProductsAsync();
        // Add a new product
        Task AddProductAsync(Product product);
        // Update an existing product
        Task UpdateProductAsync(Product product);
        // Delete a product by Id
        Task DeleteProductAsync(long id);
    }
}
