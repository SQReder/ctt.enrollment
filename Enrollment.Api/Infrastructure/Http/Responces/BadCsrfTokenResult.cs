namespace Enrollment.Api.Infrastructure.Http.Responces
{
    public class BadCsrfTokenResult : ErrorResult
    {
        public BadCsrfTokenResult() : base("BadCsrfToken")
        {
        }
    }
}