using System;
using System.Threading.Tasks;
using Enrollment.Model;
using Enrollment.Web.Database;
using Enrollment.Web.Infrastructure.Http.Responces;
using Enrollment.Web.Infrastructure.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Enrollment.Web.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _dataContext;

        public ProfileController(UserManager<ApplicationUser> userManager, ApplicationDbContext dataContext)
        {
            _userManager = userManager;
            _dataContext = dataContext;
        }

        #region Angular views

        [HttpGet]
        public IActionResult Index() => View();

        [HttpGet]
        public IActionResult ViewLayout() => View();

        [HttpGet]
        public IActionResult InfoLayout() => View();

        [HttpGet]
        public IActionResult ChildListLayout() => View();

        [HttpGet]
        public IActionResult ChildItemLayout() => View();

        [HttpGet]
        public IActionResult ChildItemEditorLayout() => View();

        #endregion


        [HttpGet]
        public async Task<IActionResult> GetCurrentUser()
        {
            GenericResult result;

            try
            {
                var user = await GetCurrentUserAsync();
                var claims = await _userManager.GetClaimsAsync(user);
                result = new UserViewModelResult(await UserViewModel.CreateAsync(_userManager, user));
            }
            catch (Exception e)
            {
                result = new ErrorResult(e);
            }

            return new ObjectResult(result);
        }

        [HttpGet]
        private async Task<ApplicationUser> GetCurrentUserAsync()
        {
            return await _userManager.FindByIdAsync(_userManager.GetUserId(HttpContext.User));
        }


    }
}
