using System.Security.Claims;
using System.Threading.Tasks;
using Enrollment.Model;
using Microsoft.AspNetCore.Identity;

namespace Enrollment.Api.Infrastructure.Identity
{
    public class UserManagerHelper: IUserManagerHelper
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserManagerHelper(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public string GetUserId(ClaimsPrincipal principal)
        {
            return _userManager.GetUserId(principal);
        }

        public Task<ApplicationUser> FindByIdAsync(string userId)
        {
            return _userManager.FindByIdAsync(userId);
        }

        public Task<ApplicationUser> FindByNameAsync(string userId)
        {
            return _userManager.FindByNameAsync(userId);
        }
    }
}
