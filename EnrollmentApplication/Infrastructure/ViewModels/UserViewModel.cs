using System.Linq;
using System.Threading.Tasks;
using Enrollment.EntityFramework.Identity.Model;
using Microsoft.AspNet.Identity;

namespace EnrollmentApplication.Infrastructure.ViewModels
{
    public class UserViewModel
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        public string Job { get; set; }
        public string JobPosition { get; set; }

        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public static async Task<UserViewModel> CreateAsync(UserManager<ApplicationUser> userManager, ApplicationUser user)
        {
            var claims = await userManager.GetClaimsAsync(user);
            return new UserViewModel
            {
                FirstName = user.FirstName,
                MiddleName = user.MiddleName,
                LastName = user.LastName,
                Job = claims.FirstOrDefault(c => c.Type == "job")?.Value,
                JobPosition = claims.FirstOrDefault(c => c.Type == "jobPosition")?.Value,
                Email = user.Email,
                Phone = user.PhoneNumber,
                Address = claims.FirstOrDefault(c => c.Type == "address")?.Value,
            };
        }
    }
}