using Enrollment.Api.Infrastructure.Http.Responces;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Enrollment.Api.Controllers
{
    [AllowAnonymous]
    [Route("api/token")]
    public class TokenController : Controller
    {
        private readonly IAntiforgery _antiforgery;

        public TokenController(IAntiforgery antiforgery)
        {
            _antiforgery = antiforgery;
        }

        [HttpGet]
        [Route("update")]
        public GenericResult Update()
        {

            var tokens = _antiforgery.GetAndStoreTokens(HttpContext);
            HttpContext.Response.Cookies.Append("XSRF-TOKEN", tokens.RequestToken,
                new CookieOptions { HttpOnly = false });

            return new SuccessResult();
        }
    }
}