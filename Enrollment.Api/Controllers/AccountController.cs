using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Enrollment.Api.Infrastructure.Exceptions;
using Enrollment.Api.Infrastructure.Http.Requests;
using Enrollment.Api.Infrastructure.Http.Responces;
using Enrollment.Api.Infrastructure.ViewModels;
using Enrollment.Api.Infrastructure.XsrfProtection;
using Enrollment.EntityFramework;
using Enrollment.Model;
using Enrollment.ServiceLayer;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace Enrollment.Api.Controllers
{
    [AllowAnonymous]
    [Route("api/account")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _dbContext;
        private readonly IRandomIdGenerator _randomIdGenerator;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ApplicationDbContext dbContext,
            IRandomIdGenerator randomIdGenerator
        )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _dbContext = dbContext;
            _randomIdGenerator = randomIdGenerator;
        }

        [HttpPost("login")]
        [ValidateCsrfToken]
        public async Task<GenericResult> Login([FromBody] LoginModel model)
        {
            GenericResult authenticationResult;

            try
            {
                var username = model.Username.ToLowerInvariant();

                var user = await _userManager.FindByNameAsync(username);
                if (user == null)
                {
                    throw new NotFoundException("User not found");
                }
                var result =
                    await
                        _signInManager.PasswordSignInAsync(username, model.Password, model.RememberMe,
                            lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    IList<string> roles;
                    if (_userManager.SupportsUserRole)
                    {
                        roles = await _userManager.GetRolesAsync(user);
                    }
                    else
                    {
                        roles = new List<string>();
                    }
                    authenticationResult = new AuthenticationResult
                    {
                        Succeeded = true,
                        Roles = roles,
                        Id = user.Id,
                        Message = "Authentication succeeded",
                    };
                }
                else
                {
                    throw new Exception("Invalid login or password");
                }
            }
            catch (Exception ex)
            {
                authenticationResult = new ErrorResult(ex);
            }

            return authenticationResult;
        }

        [HttpPost]
        [ValidateCsrfToken]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            GenericResult logoutResult;

            try
            {
                await HttpContext.Authentication.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                logoutResult = new SuccessResult("Logout succeeded");
            }
            catch (Exception ex)
            {
                logoutResult = new ErrorResult(ex);
            }
            return new ObjectResult(logoutResult);
        }

        [HttpPost("registration")]
        [ValidateCsrfToken]
        public async Task<GenericResult> Registration([FromBody] RegistrationRequest model)
        {
            GenericResult registrationResult;

            try
            {
                if (ModelState.IsValid)
                {
                    var user = new ApplicationUser
                    {
                        Id = Guid.NewGuid(),
                        PhoneNumber = "+" + model.Username,
                        UserName = model.Username,
                        NormalizedUserName = model.Username.ToUpperInvariant()
                    };

                    var result = await _userManager.CreateAsync(user, model.Password);

                    await _userManager.AddToRoleAsync(user, "Trustee");

                    if (result.Succeeded)
                    {
                        //var trustee = CreateTrusteeForUser(model, user);

                        //var repository = _dbContext.Repository<Trustee>();
                        //repository.Add(trustee);

                        //// handle id collisions
                        //try
                        //{
                        //    await _dbContext.SaveChangesAsync();
                        //}
                        //catch (Exception e)
                        //{
                        //    var sqlException = e.GetBaseException() as SqlException;
                        //    if (sqlException?.Number == 2627)
                        //    {
                        //        trustee.Id = Guid.NewGuid();
                        //        trustee.AlternateId = _randomIdGenerator.Generate();
                        //        await _dbContext.SaveChangesAsync();
                        //    }
                        //}

                        await _signInManager.SignInAsync(user, isPersistent: false);
                        registrationResult = new SuccessResult();
                    }
                    else
                    {
                        throw new Exception(result.ToString());
                    }
                }
                else
                {
                    var message = ModelState.Values
                        .Where(x => x.ValidationState == ModelValidationState.Invalid)
                        .SelectMany(x => x.Errors)
                        .Aggregate("", (s, error) => s + error.ErrorMessage + "\n");
                    throw new Exception(message);
                }
            }
            catch (Exception ex)
            {

                registrationResult = new ErrorResult(ex);
            }

            return registrationResult;
        }

        [HttpPost("checkUsername")]
        [ValidateCsrfToken]
        public async Task<GenericResult> CheckUsername([FromBody] UsernameCheckRequest model)
        {
            GenericResult result;

            try
            {
                var user = await _userManager.FindByNameAsync(model.Username);

                result = new CheckUsernameResponce(user != null);
            }
            catch (Exception e)
            {
                result = new ErrorResult(e);
            }

            return result;
        }

        [HttpGet("users")]
        [Authorize(Roles = "Admin")]
        public async Task<GenericResult> ListUsers()
        {
            GenericResult result;

            try
            {
                var users = await _userManager.Users.ToListAsync();

                var shortInfos = new List<UserShortInfo>();

                foreach (var user in users)
                {
                    var userShortInfo = await UserShortInfo.Create(_userManager, user);
                    shortInfos.Add(userShortInfo);
                }
                
                result = new ListUsersResult
                {
                    Users = shortInfos
                };
            }
            catch (Exception e)
            {
                result = new ErrorResult(e);
            }

            return result;
        }
    }

    public class UsernameCheckRequest
    {
        public string Username { get; set; }
    }

    public class CheckUsernameResponce: SuccessResult
    {
        public bool IsUsed { get; set; }

        public CheckUsernameResponce(bool isUsed)
        {
            IsUsed = isUsed;
        }
    }

    public class ListUsersResult: SuccessResult
    {
        public IEnumerable<UserShortInfo> Users { get; set; }
    }

    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public string ReturnUrl { get; set; }
    }
}
