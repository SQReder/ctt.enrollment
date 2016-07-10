using Enrollment.Web.Infrastructure.ViewModels;

namespace Enrollment.Web.Infrastructure.Http.Responces
{
    public class ListEnrolleesResult : SuccessResult
    {
        public EnrolleeViewModel[] Enrollees { get; set; }

        public ListEnrolleesResult(EnrolleeViewModel[] viewModels)
        {
            Enrollees = viewModels;
        }
    }
}