﻿using System;
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



        //// GET: /request-lines
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<RequestLine>>> GetAllRequestLines()
        //{
        //    return await _context.RequestLines.ToListAsync();
        //}



        /*  
         *  
         *  THIS IS THE PROBLEM AREA.  SQL IS WRITING BAD CODE, PULLING 
         *  FROM REQUEST WHERE IT SHOULD BE PULLING FROM REQUESTLINE
         *  
         */

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RequestLine>>> GetAllRequestLines()
        {
            var rl = await _context.RequestLines
                .Include(rl => rl.Request).ThenInclude(r => r.User)
                .Include(rl => rl.Product).ThenInclude(p => p.Vendor)
                .ToListAsync();
            return Ok(rl);
        }
        



        // GET: /request-lines/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RequestLine>> GetRequestLineById(int id)
        {
            var requestLine = await _context.RequestLines
                .FindAsync(id);

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
            if (id != requestLine.ID)
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

            return CreatedAtAction("GetRequestLine", new { id = requestLine.ID }, requestLine);
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
            return _context.RequestLines.Any(e => e.ID == id);
        }



        private async void RecalculateTotal (int requestID)
        {
            var theTotal = await _context.RequestLines
                .Where(rl => rl.RequestID == requestID)
                .Include(rl => rl.Product)
                .Select(rl => new { linetotal = rl.Quantity * rl.Product.Price })
                .SumAsync(s => s.linetotal);
          
            // find the request
            var theRequest = await _context.Requests.FindAsync(requestID);

            // update the request

            theRequest.Total = theTotal;

            // save changes()
           
                await _context.SaveChangesAsync();
          
         


            throw new NotImplementedException();
            
        }


    }
}
