using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Final_Project_4inRow_Server.Data;
using Final_Project_4inRow_Server.Models;
using Microsoft.EntityFrameworkCore;

namespace Final_Project_4inRow_Server.Pages.SignUp
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
        public Credentials Credentials { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Credentials == null || Credentials == null)
            {
                return Page();
            }

            // Check if the password already exists in the database
            bool passwordExists = await _context.Credentials.AnyAsync(c => c.Password == Credentials.Password);
            if (passwordExists)
            {
                ModelState.AddModelError(string.Empty, "This password has already been used."); // Add error message without field name
                return Page();
            }

            _context.Credentials.Add(Credentials);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

    }
}
