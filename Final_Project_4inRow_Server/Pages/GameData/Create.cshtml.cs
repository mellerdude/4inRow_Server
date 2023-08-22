using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Final_Project_4inRow_Server.Data;
using Final_Project_4inRow_Server.Models;

namespace Final_Project_4inRow_Server.Pages.GameData
{
    public class CreateModel : PageModel
    {
        private readonly Final_Project_4inRow_Server.Data.UserContext _context;

        public CreateModel(Final_Project_4inRow_Server.Data.UserContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Scores Scores { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Scores == null || Scores == null)
            {
                return Page();
            }

            _context.Scores.Add(Scores);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
