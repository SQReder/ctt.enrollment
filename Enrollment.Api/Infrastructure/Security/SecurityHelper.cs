using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Enrollment.Model;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Twilio;

namespace Enrollment.Api.Infrastructure.Security
{
    [UsedImplicitly]
    public class SecurityHelper
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly ILogger<SecurityHelper> _logger;

        public SecurityHelper(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole<Guid>> roleManager,
            ILogger<SecurityHelper> logger
            )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        public async Task InitializeSecurity()
        {
            await InitTrusteeRole();
            await InitAdminRole();
            await CreateAdminUser();
        }

        private async Task InitAdminRole()
        {
            const string roleName = "Admin";
            var adminRole = await _roleManager.FindByNameAsync(roleName);

            if (adminRole == null)
            {
                adminRole = new IdentityRole<Guid> { Name = roleName };
                var identityResult = await _roleManager.CreateAsync(adminRole);
                if (!identityResult.Succeeded)
                {
                    throw new Exception(identityResult.ToString());
                }
            }

            var claims = new List<Claim>
            {
                new Claim(Claims.Permissions.Type, Claims.Permissions.Trustee.Create),
                new Claim(Claims.Permissions.Type, Claims.Permissions.Trustee.ReadAny),
                new Claim(Claims.Permissions.Type, Claims.Permissions.Trustee.UpdateAny),
                new Claim(Claims.Permissions.Type, Claims.Permissions.Trustee.DeleteAny),
            };

            await AddClaims(adminRole, claims);
        }

        private async Task CreateAdminUser()
        {
            var user = await _userManager.FindByNameAsync("admin");

            if (user == null)
            {
                var applicationUser = new ApplicationUser
                {
                    Email = "admin@cttit.ru",
                    Id = Guid.Parse("deadbeef-dead-beef-dead-beefdeadbeef"),
                    UserName = "admin"
                };
                var result = await _userManager.CreateAsync(applicationUser, "p@55w0rD");
                if (!result.Succeeded)
                {
                    var array = result.Errors.Select(x => x.Code + ": " + x.Description).ToArray();
                    var s = string.Join("\n", array);
                    throw new Exception("Cannot create admin user:\n" + s);
                }
            }

            var adminRole = await _roleManager.FindByNameAsync("Admin");

            if (!await _userManager.IsInRoleAsync(user, adminRole.Name))
            {
                await _userManager.AddToRoleAsync(user, adminRole.Name);
            }
        }

        private async Task InitTrusteeRole()
        {
            const string roleName = "Trustee";
            var trusteeRole = await _roleManager.FindByNameAsync(roleName);

            if (trusteeRole == null)
            {
                trusteeRole = new IdentityRole<Guid> { Name = roleName };
                var identityResult = await _roleManager.CreateAsync(trusteeRole);
                if (!identityResult.Succeeded)
                {
                    throw new Exception(identityResult.ToString());
                }
            }

            var claims = new List<Claim>
            {
                new Claim(Claims.Permissions.Type, Claims.Permissions.Trustee.Create),
                new Claim(Claims.Permissions.Type, Claims.Permissions.Trustee.ReadOwned),
                new Claim(Claims.Permissions.Type, Claims.Permissions.Trustee.UpdateOwned),
                new Claim(Claims.Permissions.Type, Claims.Permissions.Trustee.DeleteOwned),
            };

            await AddClaims(trusteeRole, claims);
        }

        private async Task AddClaims(IdentityRole<Guid> role, IEnumerable<Claim> claims)
        {
            foreach (var claim in claims)
            {
                if (!role.Claims.Any(roleClaim =>
                    roleClaim.ClaimType == claim.Type
                    && roleClaim.ClaimValue == claim.Value))
                {
                    var result = await _roleManager.AddClaimAsync(role, claim);
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.ToString());
                    }
                }

            }
        }
    }
}