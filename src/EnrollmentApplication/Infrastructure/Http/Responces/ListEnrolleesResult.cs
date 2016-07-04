using EnrollmentApplication.Infrastructure.ViewModels;

namespace EnrollmentApplication.Infrastructure.Http.Responces
{
    public class ListEnrolleesResult : GenericResult
    {
        public EnrolleeViewModel[] Enrollees { get; set; }
    }
}