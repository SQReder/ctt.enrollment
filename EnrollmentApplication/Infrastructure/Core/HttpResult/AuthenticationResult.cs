using System.Collections.Generic;

namespace EnrollmentApplication.Infrastructure.Core.HttpResult
{
    public class AuthenticationResult : GenericResult
    {
        public IEnumerable<string> Roles { get; set; }
    }
}