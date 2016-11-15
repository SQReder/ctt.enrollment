using System;
using System.Threading.Tasks;
using Enrollment.EntityFramework;
using Enrollment.Model;
using JetBrains.Annotations;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Enrollment.Web.Controllers.WebApi
{
    public class ManageController: BaseController
    {
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;

        public ManageController(
            ApplicationDbContext dbContext, 
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole<Guid>> roleManager
            )
            : base(dbContext, userManager)
        {
            _roleManager = roleManager;
        }

        [HttpGet]
        public async Task<IActionResult> Setup(string password)
        {
            if (password != "1d5c79c087a64efa8cfb4b53091f6f7e")
                RedirectToAction("Index", "Home");

            var user = await UserManager.FindByIdAsync(GetCurrentUserId().ToString());

            return new ObjectResult(user != null);
        }
    }
}
