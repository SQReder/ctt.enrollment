using System;
using System.Threading.Tasks;
using Enrollment.Api.Infrastructure.Http.Responces;
using Enrollment.Api.Infrastructure.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Enrollment.Api.Controllers
{
    [AllowAnonymous]
    [Route("setup")]
    public class SetupController
    {
        private readonly SecurityHelper _securityHelper;

        public SetupController(
            SecurityHelper securityHelper
        )
        {
            _securityHelper = securityHelper;
        }

        [HttpGet("init")]
        public async Task<GenericResult> Init()
        {
            GenericResult result;

            try
            {
                await _securityHelper.InitializeSecurity();

                result = new SuccessResult("Initialization complete");
            }
            catch (Exception e)
            {
                result = new ErrorResult(e);
            }

            return result;
        }
    }
}