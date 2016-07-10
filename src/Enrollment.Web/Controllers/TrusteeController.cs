using System;
using Enrollment.Web.Infrastructure.Http.Responces;
using Microsoft.AspNetCore.Mvc;

namespace Enrollment.Web.Controllers
{
    public class TrusteeController : Controller
    {
        public IActionResult Get(Guid id)
        {
            GenericResult result;

            try
            {
                result = new GetTrusteeResult(null);
            }
            catch (Exception e)
            {
                result = new ErrorResult(e);
            }

            return new ObjectResult(result);
        }
    }
}
