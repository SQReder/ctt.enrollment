using Enrollment.Web.Infrastructure.ViewModels;

namespace Enrollment.Web.Infrastructure.Http.Responces
{
    public class GetEnrolleeResult : GenericResult
    {
        public EnrolleeViewModel Enrollee { get; set; }

        public GetEnrolleeResult() : base(true)
        {
        }
    }
}