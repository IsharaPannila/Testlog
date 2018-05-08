using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TestAspnetcorewebapplication1.Quickstart 
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string Name { get; set; }

    }
}