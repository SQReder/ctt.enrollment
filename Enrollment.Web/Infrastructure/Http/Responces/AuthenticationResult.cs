using System;
using System.Collections.Generic;

namespace Enrollment.Web.Infrastructure.Http.Responces
{
    public class AuthenticationResult : SuccessResult
    {
        public IEnumerable<Guid> Roles { get; set; }
    }
}