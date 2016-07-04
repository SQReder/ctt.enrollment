using System;
using System.Threading.Tasks;
using EnrollmentApplication.Database;
using EnrollmentApplication.Infrastructure.Http.Responces;
using EnrollmentApplication.Infrastructure.ViewModels;
using EnrollmentApplication.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EnrollmentApplication.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly EnrollmentDbContext _dataContext;

        public ProfileController(UserManager<ApplicationUser> userManager, EnrollmentDbContext dataContext)
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
                result = new UserViewModelResult
                {
                    Succeeded = true,
                    User = await UserViewModel.CreateAsync(_userManager, user),
                };
            }
            catch (Exception e)
            {
                result = new GenericResult
                {
                    Succeeded = false,
                    Message = e.GetBaseException().Message
                };
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
