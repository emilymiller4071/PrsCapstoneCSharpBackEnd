using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrsBackEndCSharp.Models;

namespace PrsBackEndCSharp.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly PrsContext _context;

        public ProductsController(PrsContext context)
        {
            _context = context;
        }

        // GET: /Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll()
        {
            var product = await _context.Products.Include(p => p.Vendor).ToListAsync();



            return product;
        }




        // GET: /Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetById(int id)
        {
            var product = await _context.Products.Include(p => p.Vendor).Where(p => p.ID == id).SingleOrDefaultAsync();

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }




        // PUT: /Products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Product product)
        {
            //if (id != product.Id)
            //{
            //    return BadRequest();
            //}

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(product.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }




        // POST: /Products
        [HttpPost("")]
        public async Task<ActionResult<Product>> Create(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return product;
        }




        // DELETE: /Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }



        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ID == id);
        }
    }
}
