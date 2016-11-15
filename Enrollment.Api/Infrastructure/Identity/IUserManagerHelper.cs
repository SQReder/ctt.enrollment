using System.Security.Claims;
using System.Threading.Tasks;
using Enrollment.Model;

namespace Enrollment.Api.Infrastructure.Identity
{
    public interface IUserManagerHelper
    {
        string GetUserId(ClaimsPrincipal principal);
        Task<ApplicationUser> FindByIdAsync(string userId);
        Task<ApplicationUser> FindByNameAsync(string userId);
    }
}