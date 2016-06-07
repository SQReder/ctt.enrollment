using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Enrollment.DataAccess;
using Enrollment.EntityFramework;
using Enrollment.EntityFramework.Identity;
using Enrollment.EntityFramework.Identity.Model;
using Enrollment.Model;
using EnrollmentApplication.Infrastructure.Http.Responces;
using EnrollmentApplication.Infrastructure.Model;
using EnrollmentApplication.Infrastructure.ViewModels;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;

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
            return await _userManager.FindByIdAsync(HttpContext.User.GetUserId());
        }

        [HttpGet]
        public IActionResult ListChildren()
        {
            GenericResult result;
            try
            {
                result = new ListChildrenResult
                {
                    Succeeded = true,
                    Children = new ChildViewModel[]
                    {
                        new ChildViewModel()
                        {
                            Id = new Guid(),
                            FirstName = "Никита",
                            MiddleName = "Васильевич",
                            LastName = "Пупкин",
                            AddressSameAsParent = true,
                            RelationshipDegree = RelationshipDegreeEnum.Child,
                        },
                    },
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddChild()
        {
            GenericResult result;
            try
            {
                var repository = _dataContext.Repository<Enrollee>();
                var child = repository.Add(new Enrollee
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Никита",
                    MiddleName = "Васильевич",
                    LastName = "Пупкин",
                    AddressSameAsParent = true,
                    RelationshipDegree = RelationshipDegreeEnum.Child,
                });
                await _dataContext.SaveChangesAsync();
                result = new GenericResult()
                {
                    Succeeded = true
                };
            }
            catch (Exception e)
            {
                result = new GenericResult()
                {
                    Succeeded = false,
                    Message = e.GetBaseException().Message
                };   
            }

            return new ObjectResult(result);
        }
    }
}
