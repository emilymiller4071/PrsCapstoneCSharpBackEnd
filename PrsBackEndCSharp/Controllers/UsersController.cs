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
        public async Task<ActionResult<Object>> LoginUser([FromBody] UserPasswordObject upo)
        {
            var user = await _context.Users.Where(u => u.UserName == upo.username && u.Password == upo.password).FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound();  // 404
            }

            return user;  // best practice: only return what's needed!

            // best practice
            //return new { Firstname = user.FirstName, Lastname = user.LastName, Id = user.ID, IsAdmin = user.IsAdmin };
        }




        // GET: /Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }


        // GET: /Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            //var user1 = await _context.Users.Where(u => u.ID == id).FirstOrDefaultAsync();


            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: /Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
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
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.ID }, user);
        }

        // DELETE: /Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
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
