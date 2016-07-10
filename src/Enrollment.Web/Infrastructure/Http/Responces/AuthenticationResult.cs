using System.Collections.Generic;

namespace Enrollment.Web.Infrastructure.Http.Responces
{
    public class AuthenticationResult : SuccessResult
    {
        public IEnumerable<string> Roles { get; set; }
    }
}