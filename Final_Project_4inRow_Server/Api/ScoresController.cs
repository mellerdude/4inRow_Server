using Final_Project_4inRow_Server.Data;
using Final_Project_4inRow_Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Final_Project_4inRow_Server.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScoresController : ControllerBase
    {
        private readonly UserContext _context;

        public ScoresController(UserContext context)
        {
            _context = context;
        }
        // GET: api/Scores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Scores>>> GetCredentials()
        {
            if (_context.Scores == null)
            {
                return NotFound();
            }
            return await _context.Scores.ToListAsync();
        }

        // POST: api/Scores
        [HttpPost]
        public async Task<ActionResult<Scores>> PostTblProducts(Scores scores)
        {
            if (_context.Scores == null)
            {
                return Problem("Entity set 'userContext.Scores'  is null.");
            }
            _context.Scores.Add(scores);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetScores", new { id = scores.Id }, scores);
        }
    }
}
