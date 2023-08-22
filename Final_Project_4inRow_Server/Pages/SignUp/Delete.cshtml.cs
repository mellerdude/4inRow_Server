using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Final_Project_4inRow_Server.Data;
using Final_Project_4inRow_Server.Models;

namespace Final_Project_4inRow_Server.Pages.SignUp
{
    public class DeleteModel : PageModel
    {
        private readonly Final_Project_4inRow_Server.Data.UserContext _context;

        public DeleteModel(Final_Project_4inRow_Server.Data.UserContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Credentials Credentials { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Credentials == null)
            {
                return NotFound();
            }

            var credentials = await _context.Credentials.FirstOrDefaultAsync(m => m.Id == id);

            if (credentials == null)
            {
                return NotFound();
            }
            else 
            {
                Credentials = credentials;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Credentials == null)
            {
                return NotFound();
            }
            var credentials = await _context.Credentials.FindAsync(id);

            if (credentials != null)
            {
                Credentials = credentials;
                _context.Credentials.Remove(Credentials);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
