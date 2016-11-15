using System;
using System.Linq;
using System.Threading.Tasks;
using Enrollment.Model;
using Microsoft.AspNetCore.Identity;

namespace Enrollment.Api.Infrastructure.ViewModels
{
    public class UserShortInfo
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string[] Roles { get; set; }

        public static async Task<UserShortInfo> Create(UserManager<ApplicationUser> userManager, ApplicationUser user)
        {
            var roles = await userManager.GetRolesAsync(user);
            return new UserShortInfo
            {
                Id = user.Id,
                Username = user.UserName,
                Roles = roles.ToArray(),
            };
        }
    }
}