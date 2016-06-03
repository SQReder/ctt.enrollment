using EnrollmentApplication.Infrastructure.ViewModels;

namespace EnrollmentApplication.Infrastructure.Core.HttpResult
{
    public class UserViewModelResult : GenericResult
    {
        public UserViewModel User { get; set; }
    }
}