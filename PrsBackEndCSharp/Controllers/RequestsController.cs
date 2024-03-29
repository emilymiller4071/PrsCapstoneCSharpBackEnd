﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrsBackEndCSharp.Models;


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
        public async Task<ActionResult<IEnumerable<Request>>> GetAll()
        {
            return await _context.Requests.Include(r => r.User).ToListAsync();
        }




        // GET: /Requests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Request>> GetById(int id)
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




        // PUT: /Requests/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] Request UpdatedRequest)
        {
            var request = await _context.Requests.FindAsync(UpdatedRequest.ID);

            if (request == null)
            {
                return NotFound();
            }

            if (request.ID != UpdatedRequest.ID)
            {
                return BadRequest();
            }

            _context.Entry(request).CurrentValues.SetValues(UpdatedRequest);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestExists(UpdatedRequest.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(request);
        }




        // POST: /Requests
        [HttpPost]
        public async Task<ActionResult<Request>> Create([FromBody] Request request)
        {
            request.Status = Models.Request.STATUSNEW;
            _context.Requests.Add(request);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetById", new { id = request.ID }, request);
        }




        // DELETE: /Requests/5
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




        // APPROVE: /Requests/approve
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
                request.Status = Models.Request.STATUSAPPROVED;
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
                request.Status = Models.Request.STATUSAPPROVED;
                request.SubmittedDate = DateTime.Now;
            }
            else
            {
                request.Status = Models.Request.STATUSINREVIEW;
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
                request.Status = Models.Request.STATUSREJECTED;
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
                request.Status = Models.Request.STATUSREOPENED;
            }
            await _context.SaveChangesAsync();

            return request;
        }




        [HttpGet]
        [Route("list-review/{UserID}")]
        public async Task<ActionResult<IEnumerable<Request>>> GetAllForReview(int UserID) 
        {
            return await _context.Requests
                    .Where(r => r.Status == Models.Request.STATUSINREVIEW && UserID != r.UserID 
                    && (r.User.IsAdmin || r.User.IsReviewer))
                    .ToListAsync();
        }

    }
}
