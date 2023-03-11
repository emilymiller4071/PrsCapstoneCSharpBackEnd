using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrsBackEndCSharp.Models;

namespace PrsBackEndCSharp.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly PrsContext _context;

        public UsersController(PrsContext context)
        {
            _context = context;
        }




        [Route("/login")]
        [HttpPost]
        public async Task<ActionResult<Object>> Login([FromBody] UserPasswordObject upo)
        {
            var user = await _context.Users.Where(u => u.UserName == upo.username && u.Password == upo.password).FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound(); 
            }

            if (user.Password != upo.password) 
            {
                return NotFound();
            }


            return new 
            { 
                Id = user.ID, 
                FirstName = user.FirstName, 
                LastName = user.LastName,
                IsReviewer = user.IsReviewer, 
                IsAdmin = user.IsAdmin
            };
            
        }




        // GETALL: /Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }




        // GETBYID: /Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetById(int id)
        {
            var user = await _context.Users.FindAsync(id);
            


            if (user == null)
            {
                return NotFound();
            }

            var userDTO = new
            {
                Id = user.ID,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Phone = user.Phone,
                Email = user.Email,
                IsReviewer = user.IsReviewer,
                IsAdmin = user.IsAdmin
            };

            return userDTO;
        }




        // PUT: /Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, User user)
        {
            if (id != user.ID)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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




        // POST: /Users
        [HttpPost]
        public async Task<ActionResult<User>> Create(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetById", new { id = user.ID }, user);
        }




        // DELETE: /Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }




        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.ID == id);
        }
    }
}
