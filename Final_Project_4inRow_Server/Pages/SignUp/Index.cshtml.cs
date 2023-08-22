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
    public class IndexModel : PageModel
    {
        private readonly Final_Project_4inRow_Server.Data.UserContext _context;

        public IndexModel(Final_Project_4inRow_Server.Data.UserContext context)
        {
            _context = context;
        }

        public IList<Credentials> Credentials { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Credentials != null)
            {
                Credentials = await _context.Credentials.ToListAsync();
            }
        }
    }
}
