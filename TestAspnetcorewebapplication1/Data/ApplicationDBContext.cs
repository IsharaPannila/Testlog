using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAspnetcorewebapplication1.Quickstart;
using TestAspnetcorewebapplication1.Quickstart.Account;

namespace TestAspnetcorewebapplication1
{
    public class ApplicationDBContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) :base(options) { }
       
    }
}
