using System;
using System.Collections.Generic;

namespace Enrollment.Api.Infrastructure.Http.Responces
{
    public class AuthenticationResult : SuccessResult
    {
        public IEnumerable<string> Roles { get; set; }
        public Guid Id { get; set; }

    }
}