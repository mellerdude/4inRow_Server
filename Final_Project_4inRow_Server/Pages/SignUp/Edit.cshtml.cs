using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Final_Project_4inRow_Server.Data;
using Final_Project_4inRow_Server.Models;

namespace Final_Project_4inRow_Server.Pages.SignUp
{
    public class EditModel : PageModel
    {
        private readonly Final_Project_4inRow_Server.Data.UserContext _context;

        public EditModel(Final_Project_4inRow_Server.Data.UserContext context)
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

            var credentials =  await _context.Credentials.FirstOrDefaultAsync(m => m.Id == id);
            if (credentials == null)
            {
                return NotFound();
            }
            Credentials = credentials;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Credentials).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CredentialsExists(Credentials.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool CredentialsExists(int id)
        {
          return (_context.Credentials?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
