using Final_Project_4inRow_Server.Data;
using Final_Project_4inRow_Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Final_Project_4inRow_Server.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly UserContext _context;

        public UsersController(UserContext context)
        {
            _context = context;
        }
        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Credentials>>> GetCredentials()
        {
            if (_context.Credentials == null)
            {
                return NotFound();
            }
            return await _context.Credentials.ToListAsync();
        }

        // GET: api/Users/RandomNumber
        [HttpGet("RandomNumber")]
        public ActionResult<int> GetRandomNumber()
        {
            Random rand = new Random();
            int number = rand.Next(0, 7); // This will generate a random number between 0 and 7.
            return number;
        }
    }
}
