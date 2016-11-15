using System;
using System.Threading.Tasks;
using Enrollment.EntityFramework;
using Enrollment.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Enrollment.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        protected readonly ApplicationDbContext DataContext;
        protected readonly UserManager<ApplicationUser> UserManager;

        protected BaseController(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            DataContext = dbContext;
            UserManager = userManager;
        }

        protected Guid GetCurrentUserId()
        {
            return Guid.Parse(UserManager.GetUserId(HttpContext.User));
        }

        protected async Task<ApplicationUser> GetCurrentUserAsync()
        {
            return await UserManager.FindByIdAsync(UserManager.GetUserId(HttpContext.User));
        }
    }
}