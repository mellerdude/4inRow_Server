using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace Final_Project_4inRow_Server.Data
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {
        }

        public DbSet<Models.Credentials> Credentials { get; set; } = default!;
        public DbSet<Models.Scores> Scores { get; set; } = default!;

    }
}
