using EnrollmentApplication.Infrastructure.ViewModels;

namespace EnrollmentApplication.Infrastructure.Http.Responces
{
    public class GetEnrolleeResult : GenericResult
    {
        public EnrolleeViewModel Enrollee { get; set; }
    }
}