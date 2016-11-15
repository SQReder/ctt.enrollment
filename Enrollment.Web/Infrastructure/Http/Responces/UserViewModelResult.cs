using Enrollment.Web.Infrastructure.ViewModels;

namespace Enrollment.Web.Infrastructure.Http.Responces
{
    public class UserViewModelResult : SuccessResult
    {
        public UserViewModel User { get; set; }

        public UserViewModelResult(UserViewModel user)
        {
            User = user;
        }
    }
}