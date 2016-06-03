using Microsoft.AspNet.Identity.EntityFramework;

namespace Enrollment.EntityFramework.Identity.Model
{
    public class ApplicationUser: IdentityUser
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
    }
}