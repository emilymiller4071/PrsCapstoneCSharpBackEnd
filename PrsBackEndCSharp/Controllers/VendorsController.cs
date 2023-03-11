using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrsBackEndCSharp.Models;

namespace PrsBackEndCSharp.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class VendorsController : ControllerBase
    {
        private readonly PrsContext _context;

        public VendorsController(PrsContext context)
        {
            _context = context;
        }

        // GET: /Vendors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vendor>>> GetAll()
        {
            return await _context.Vendors.ToListAsync();
        }




        // GET: /Vendors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Vendor>> GetById(int id)
        {
            var vendor = await _context.Vendors.FindAsync(id);

            if (vendor == null)
            {
                return NotFound();
            }

            return vendor;
        }




        // PUT: /Vendors/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Vendor vendor)
        {
            if (id != vendor.ID)
            {
                return BadRequest();
            }

            _context.Entry(vendor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VendorExists(id))
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




        // POST: /Vendors
        [HttpPost]
        public async Task<ActionResult<Vendor>> Create(Vendor vendor)
        {
            _context.Vendors.Add(vendor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetById", new { id = vendor.ID }, vendor);
        }




        // DELETE: /Vendors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var vendor = await _context.Vendors.FindAsync(id);
            if (vendor == null)
            {
                return NotFound();
            }

            _context.Vendors.Remove(vendor);
            await _context.SaveChangesAsync();

            return NoContent();
        }




        private bool VendorExists(int id)
        {
            return _context.Vendors.Any(e => e.ID == id);
        }
    }
}
