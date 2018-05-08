using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAspnetcorewebapplication1.Quickstart.Account;

namespace TestAspnetcorewebapplication1
{
    public class ApplicationDBContext : IdentityDbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) :base(options) { }
        public DbSet<TestAspnetcorewebapplication1.Quickstart.Account.RegisterViewModel> RegisterViewModel { get; set; }
    }
}
