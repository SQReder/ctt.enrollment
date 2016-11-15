using System;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Enrollment.DataAccess;
using Enrollment.EntityFramework;
using Enrollment.Model;
using Enrollment.ServiceLayer;
using Enrollment.Web.Infrastructure.Exceptions;
using Enrollment.Web.Infrastructure.Helpers;
using Enrollment.Web.Infrastructure.Http.Requests;
using Enrollment.Web.Infrastructure.Http.Responces;
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
                    throw new NotFoundException("User not found");
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
                        Id = Guid.NewGuid(),
                        Email = model.Email,
                        PhoneNumber = model.Phone,
                        UserName = model.Phone.Replace("+", "").Trim(),
                        NormalizedUserName = model.Phone.Replace("+", "").Trim()
                    };

                    var result = await _userManager.CreateAsync(user, ApplicationUserHelper.GeneratePassword(user));

                    await _userManager.AddToRoleAsync(user, "Trustee");

                    if (result.Succeeded)
                    {
                        var trustee = CreateTrusteeForUser(model, user);

                        var repository = _dbContext.Repository<Trustee>();
                        repository.Add(trustee);

                        // handle id collisions
                        try
                        {
                            await _dbContext.SaveChangesAsync();
                        }
                        catch (Exception e)
                        {
                            var sqlException = e.GetBaseException() as SqlException;
                            if (sqlException?.Number == 2627)
                            {
                                trustee.Id = Guid.NewGuid();
                                trustee.AlternateId = _randomIdGenerator.Generate();
                                await _dbContext.SaveChangesAsync();
                            }
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

        private Trustee CreateTrusteeForUser(RegistrationRequest model, ApplicationUser user)
        {
            var trustee = new Trustee
            {
                Owner = user,
                FirstName = model.FirstName,
                MiddleName = model.MiddleName,
                LastName = model.LastName,
                Id = Guid.NewGuid(),
                AlternateId = _randomIdGenerator.Generate(),
                Email = model.Email,
                Job = model.Job,
                JobPosition = model.JobPosition,
                Address = new Address {Id = Guid.NewGuid(), Raw = model.Address},
                PhoneNumber = model.Phone
            };

            return trustee;
        }
    }
}
