using EnrollmentApplication.Infrastructure.ViewModels;

namespace EnrollmentApplication.Infrastructure.Http.Responces
{
    public class UserViewModelResult : GenericResult
    {
        public UserViewModel User { get; set; }
    }
}