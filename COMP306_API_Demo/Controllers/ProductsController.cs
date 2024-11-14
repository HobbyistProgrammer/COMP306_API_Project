using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using COMP306_API_Demo.Services;
using ProductLibrary.Entities;
using COMP306_API_Demo.Models; // Make sure to adjust this to your DTOs' namespace

namespace COMP306_API_Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductsController(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductSummaryDto>>> GetProducts()
        {
            var products = await _productRepository.GetAllProductsAsync();
            var productSummaries = _mapper.Map<IEnumerable<ProductSummaryDto>>(products);
            return Ok(productSummaries);
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDetailDto>> GetProduct(long id)
        {
            var product = await _productRepository.GetProductAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            var productDetails = _mapper.Map<ProductDetailDto>(product);
            return Ok(productDetails);
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(long id, ProductCreateUpdateDto productDto)
        {
            // Map the DTO to the Product entity
            var product = _mapper.Map<Product>(productDto);
            // Set the ID directly, as it is passed in the route
            product.Id = id;

            await _productRepository.UpdateProductAsync(product);
            return NoContent();
        }

        // POST: api/Products
        [HttpPost]
        public async Task<ActionResult<ProductDetailDto>> PostProduct(ProductCreateUpdateDto productDto)
        {
            // Map the DTO to the Product entity
            var product = _mapper.Map<Product>(productDto);
            await _productRepository.AddProductAsync(product);

            // Map back to ProductDetailsDto to return detailed information in response
            var productDetails = _mapper.Map<ProductDetailDto>(product);
            return CreatedAtAction(nameof(GetProduct), new { id = productDetails.Id }, productDetails);
        }


        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(long id)
        {
            var product = await _productRepository.GetProductAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            await _productRepository.DeleteProductAsync(id);
            return NoContent();
        }
    }
}
