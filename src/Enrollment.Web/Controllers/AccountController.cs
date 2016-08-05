﻿using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Enrollment.DataAccess;
using Enrollment.Model;
using Enrollment.Web.Database;
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

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ApplicationDbContext dbContext
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _dbContext = dbContext;
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
                        Id = Guid.NewGuid(),
                        Email = model.Email,
                        PhoneNumber = model.Phone,
                        UserName = model.Phone.Replace("+", "").Trim(),
                        NormalizedUserName = model.Phone.Replace("+", "").Trim()
                    };

                    var result = await _userManager.CreateAsync(user, ApplicationUserHelper.GeneratePassword(user));
                    if (result.Succeeded)
                    {
                        var trustee = CreateTrusteeForUser(model, user);

                        var repository = _dbContext.Repository<Trustee>();
                        repository.Add(trustee);
                        await _dbContext.SaveChangesAsync();

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
