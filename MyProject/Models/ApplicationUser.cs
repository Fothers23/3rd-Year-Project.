using Microsoft.AspNetCore.Identity;

namespace MyProject.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Developer { get; set; }
        public string CompanyDescription { get; set; }
    }
}
