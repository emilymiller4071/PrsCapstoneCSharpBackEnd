using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrsBackEndCSharp.Models;

namespace PrsBackEndCSharp.Controllers
{
    [Route("/request-lines")]
    [ApiController]
    public class RequestLinesController : ControllerBase
    {
        private readonly PrsContext _context;

        public RequestLinesController(PrsContext context)
        {
            _context = context;
        }



        // GET: /request-lines
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RequestLine>>> GetAllRequestLines()
        {
            return await _context.RequestLines.ToListAsync();
        }



        // GET: /request-lines/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RequestLine>> GetRequestLineById(int id)
        {
            var requestLine = await _context.RequestLines.FindAsync(id);

            if (requestLine == null)
            {
                return NotFound();
            }

            return requestLine;
        }



        // UPDATE: /request-lines/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, RequestLine requestLine)
        {
            if (id != requestLine.Id)
            {
                return BadRequest();
            }

            _context.Entry(requestLine).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                RecalculateTotal(requestLine.RequestID);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestLineExists(id))
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



        // CREATE: /request-lines
        [HttpPost]
        public async Task<ActionResult<RequestLine>> Create(RequestLine requestLine)
        {
            _context.RequestLines.Add(requestLine);
            await _context.SaveChangesAsync();
            RecalculateTotal(requestLine.RequestID);

            return CreatedAtAction("GetRequestLine", new { id = requestLine.Id }, requestLine);
        }



        // DELETE: /request-lines/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var requestLine = await _context.RequestLines.FindAsync(id);
            if (requestLine == null)
            {
                return NotFound();
            }

            int theRequestID = requestLine.RequestID;

            _context.RequestLines.Remove(requestLine);
            await _context.SaveChangesAsync();
            
            RecalculateTotal(theRequestID);

            return NoContent();
        }



        private bool RequestLineExists(int id)
        {
            return _context.RequestLines.Any(e => e.Id == id);
        }



        void RecalculateTotal (int RequestID)
        {
            _context.RequestLines
                .Where(rl => rl.RequestID == RequestID)
                .Include(rl => rl.Product)
                .Select(rl => new { linetotal = rl.Quantity * rl.Product.Price })
                .Sum(s => s.linetotal);
          
            _context.SaveChangesAsync();
           

            throw new NotImplementedException();
            
        }


    }
}
