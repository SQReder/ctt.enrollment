using System;
using System.Security.Claims;
using System.Threading.Tasks;
using EnrollmentApplication.Infrastructure.Exceptions;
using EnrollmentApplication.Infrastructure.Helpers;
using EnrollmentApplication.Infrastructure.Http.Requests;
using EnrollmentApplication.Infrastructure.Http.Responces;
using EnrollmentApplication.Model;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;

namespace EnrollmentApplication.Controllers
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
                    authenticationResult = new GenericResult
                    {
                        Succeeded = false,
                        Message = "Invalid login"
                    };
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
                authenticationResult = new GenericResult()
                {
                    Succeeded = false,
                    Message = ex.Message
                };
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
                logoutResult = new GenericResult
                {
                    Succeeded = true,
                    Message = "Logout succeeded"
                };
            }
            catch (Exception ex)
            {
                logoutResult = new GenericResult
                {
                    Succeeded = true,
                    Message = ex.Message
                };
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
                        registrationResult = new GenericResult
                        {
                            Succeeded = true,
                        };
                    }
                    else
                    {
                        registrationResult = new GenericResult
                        {
                            Succeeded = false,
                            Message = string.Join("\n", result.Errors.Select(x => $"[{x.Code}] {x.Description}").ToArray())
                        };
                    }
                }
                else
                {
                    var message = ModelState.Values
                        .Where(x => x.ValidationState == ModelValidationState.Invalid)
                        .SelectMany(x => x.Errors)
                        .Aggregate("", (s, error) => s + error.ErrorMessage + "\n");
                    registrationResult = new GenericResult { Succeeded = false, Message = message };
                }
            }
            catch (Exception ex)
            {

                registrationResult = new GenericResult
                {
                    Succeeded = false,
                    Message = ex.GetBaseException().Message
                };
            }

            return new ObjectResult(registrationResult);
        }
    }
}
