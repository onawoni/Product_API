using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductApi.Data;
using ProductApi.Models;




namespace ProductApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly ProductDbContext _context;
        public ProductController(ProductDbContext _context)
        {
            this._context = _context;

        }
    

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _context.Products.ToListAsync();
            if (products == null)
            {
                return NotFound();
            }
            return Ok(products);
        }
        [HttpPost]
        public async Task<IActionResult> Post(Product products)
        {
            await _context.Products.AddAsync(products);
            products.Id = _context.Products.Count() + 1;
            _context.SaveChanges();
            return Ok(products);
        }
        [HttpGet]
        public async Task<IActionResult> GetId(int id)
        {
            var products = await _context.Products.FindAsync(id);
            if (products == null)
            {
                return NotFound("product with id: " + id + " not found");
            }
            return Ok(products);
        }
        [HttpPut]
        public async Task<IActionResult> Put(int id, Product updatedProduct)
        {
            var products = await _context.Products.FindAsync(id);
            if (products == null)
            {
                return NotFound("This product is not available");
            }
            products.Name = updatedProduct.Name;
            products.Description = updatedProduct.Description;
            products.Price = updatedProduct.Price;
            products.Inventory = updatedProduct.Inventory;
            _context.SaveChanges();
            return Ok(products);
        }
         
      
        
    }
}
