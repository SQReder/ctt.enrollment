using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Enrollment.Web.Infrastructure.Exceptions;
using Enrollment.Web.Infrastructure.Helpers;
using Enrollment.Web.Infrastructure.Http.Requests;
using Enrollment.Web.Infrastructure.Http.Responces;
using Enrollment.Web.Model;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Enrollment.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login() => View();

        [HttpGet]
        [AllowAnonymous]
        public IActionResult LoginPanel() => View();

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Registration() => View();

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string username, string password, bool rememberMe)
        {
            GenericResult authenticationResult;

            try
            {
                var user = await _userManager.FindByNameAsync(username);
                if (user == null)
                {
                    throw new NotFoundException();
                }
                var result =
                    await _signInManager.PasswordSignInAsync(username, password, rememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    authenticationResult = new AuthenticationResult
                    {
                        Succeeded = true,
                        Roles = user.Roles.Select(x => x.RoleId),
                        Message = "Authentication succeeded",
                    };
                }
                else
                {
                    throw new Exception("Invalid login");
                }
                //var claims = new List<Claim>();
                //var claim = new Claim(ClaimTypes.Role, "Admin", ClaimValueTypes.String, username);
                //claims.Add(claim);
                //await HttpContext.Authentication.SignInAsync(
                //    CookieAuthenticationDefaults.AuthenticationScheme,
                //    new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme)),
                //    new Microsoft.AspNet.Http.Authentication.AuthenticationProperties { IsPersistent = rememberMe });


                //authenticationResult = new AuthenticationResult
                //{
                //    Succeeded = true,
                //    Message = "Authentication succeeded",
                //    Roles = claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value)
                //};
            }
            catch (Exception ex)
            {
                authenticationResult = new ErrorResult(ex);
            }

            return new ObjectResult(authenticationResult);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Registration(RegistrationRequest model)
        {
            GenericResult registrationResult;

            try
            {
                if (ModelState.IsValid)
                {
                    var user = new ApplicationUser
                    {
                        Id = Guid.NewGuid().ToString("N"),
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        MiddleName = model.MiddleName,
                        Email = model.Email,
                        PhoneNumber = model.Phone,
                        UserName = model.Phone.Replace("+", "").Trim(),
                        NormalizedUserName = model.Phone.Replace("+", "").Trim()
                    };

                    var result = await _userManager.CreateAsync(user, ApplicationUserHelper.GeneratePassword(user));
                    if (result.Succeeded)
                    {
                        var claims = new[]
                        {
                            new Claim("address", model.Address),
                            new Claim("job", model.Job),
                            new Claim("jobPosition", model.JobPosition),
                        };

                        var identityResult = await _userManager.AddClaimsAsync(user, claims);
                        if (!identityResult.Succeeded)
                        {
                            var message = identityResult.Errors.Select(x => $"{x.Code} {x.Description}").Aggregate((a,b) =>
                                $"{a}\n{b}");
                            throw new Exception(message);
                        }

                        await _signInManager.SignInAsync(user, isPersistent: false);
                        registrationResult = new SuccessResult();
                    }
                    else
                    {
                        throw new Exception(string.Join("\n",
                            result.Errors.Select(x => $"[{x.Code}] {x.Description}").ToArray()));
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

            return new ObjectResult(registrationResult);
        }
    }
}
