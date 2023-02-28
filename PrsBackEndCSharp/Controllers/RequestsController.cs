using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.Intrinsics.X86;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Newtonsoft.Json.Linq;
using PrsBackEndCSharp.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace PrsBackEndCSharp.Controllers
{




    [Route("/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase
    {

        private readonly PrsContext _context;

        public RequestsController(PrsContext context)
        {
            _context = context;
        }



        // GET: /Requests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Request>>> GetRequests()
        {
            return await _context.Requests.Include(r => r.User).ToListAsync();
        }



        // GET: /Requests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Request>> GetRequest(int id)
        {
            //var request = await _context.Requests.FindAsync(id);

            var request = await _context.Requests.Where(r => r.ID == id)
                                                .Include(r => r.User)
                                                .FirstOrDefaultAsync();


            if (request == null)
            {
                return NotFound();
            }

            return request;
        }



        // PUT: api/Requests/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] int id, Request request)
        {
            if (id != request.ID)
            {
                return BadRequest();
            }

            _context.Entry(request).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestExists(id))
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

        // POST: /Requests
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Request>> Create([FromBody] Request request)
        {
            _context.Requests.Add(request);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRequest", new { id = request.ID }, request);
        }

        // DELETE: api/Requests/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var request = await _context.Requests.FindAsync(id);
            if (request == null)
            {
                return NotFound();
            }

            _context.Requests.Remove(request);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RequestExists(int id)
        {
            return _context.Requests.Any(e => e.ID == id);
        }




        // APPROVE: api/Requests/approve
        [HttpPut]
        [Route("approve")]
        public async Task<ActionResult<Request>> Approve([FromBody] Request ApprovedRequest)
        {
            var request = await _context.Requests.FindAsync(ApprovedRequest.ID);
            if (request == null)
            {
                return NotFound();
            }
            else
            {
                request.Status = Models.Request.StatusApproved;
            }
            await _context.SaveChangesAsync();

            return request;
        }


        [HttpPut]
        [Route("review")]
        public async Task<ActionResult<Request>> Review([FromBody] Request RequestForReview)
        {
            var request = await _context.Requests.FindAsync(RequestForReview.ID);
            if (request == null)
            {
                return NotFound();
            }
            else if (request.Total <= 50)
            {
                request.Status = Models.Request.StatusApproved;
                request.SubmittedDate = DateTime.Now;
            }
            else
            {
                request.Status = Models.Request.StatusInReview;
                request.SubmittedDate = DateTime.Now;
            }
            await _context.SaveChangesAsync();

            return request;
        }

        [HttpPut]
        [Route("reject")]
        public async Task<ActionResult<Request>> Reject([FromBody] Request RejectedRequest)
        {
            var request = await _context.Requests.FindAsync(RejectedRequest.ID);
            if (request == null)
            {
                return NotFound();
            }
            else
            {
                request.Status = Models.Request.StatusRejected;
            }
            await _context.SaveChangesAsync();

            return request;
        }


        // REOPEN: /Requests/reopen
        [HttpPut]
        [Route("reopen")]
        public async Task<ActionResult<Request>> Reopen([FromBody] Request ReopenedRequest)
        {
            var request = await _context.Requests.FindAsync(ReopenedRequest.ID);
            if (request == null)
            {
                return NotFound();
            }
            else
            {
                request.Status = Models.Request.StatusReopened;
            }
            await _context.SaveChangesAsync();

            return request;
        }

        [HttpGet]
        [Route("list-review /{UserID}")]
        public async Task<ActionResult<IEnumerable<Request>>> GetAllForReview(int UserID) 
        {
            return await _context.Requests
                    .Where(r => r.Status == Models.Request.StatusInReview && UserID != r.UserID)
                    .ToListAsync();
        }



    //    @GetMapping("/list-review/{userId}")

    //public List<Request> getAllForReview(@PathVariable int userId)
    //    {
    //        List<Request> requests = requestRepo.findByStatusAndUserIdNot(REVIEW, userId);


    //        return requests;
    //    }

        private void RecalculateTotal(int requestID)
        {
            //sum up requestlines
            //update the request
            //save changes
        }
    }
}
