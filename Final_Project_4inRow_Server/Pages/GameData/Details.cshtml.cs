using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Final_Project_4inRow_Server.Data;
using Final_Project_4inRow_Server.Models;

namespace Final_Project_4inRow_Server.Pages.GameData
{
    public class DetailsModel : PageModel
    {
        private readonly Final_Project_4inRow_Server.Data.UserContext _context;

        public DetailsModel(Final_Project_4inRow_Server.Data.UserContext context)
        {
            _context = context;
        }

      public Scores Scores { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Scores == null)
            {
                return NotFound();
            }

            var scores = await _context.Scores.FirstOrDefaultAsync(m => m.Id == id);
            if (scores == null)
            {
                return NotFound();
            }
            else 
            {
                Scores = scores;
            }
            return Page();
        }
    }
}
