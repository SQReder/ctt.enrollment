using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Enrollment.Web.Model
{
    public class ApplicationUser: IdentityUser
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
    }
}