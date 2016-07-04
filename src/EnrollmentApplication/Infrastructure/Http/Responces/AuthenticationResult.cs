using System.Collections.Generic;

namespace EnrollmentApplication.Infrastructure.Http.Responces
{
    public class AuthenticationResult : GenericResult
    {
        public IEnumerable<string> Roles { get; set; }
    }
}