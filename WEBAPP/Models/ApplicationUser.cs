using Microsoft.AspNetCore.Identity;
using Microsoft.Build.Framework;

namespace WEBAPP.Models
{
    public class ApplicationUser: IdentityUser
    {
        [Required]
        public string Name { get; set; }
        public string? Street_Address { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? Post_code { get; set; }

    }
}
