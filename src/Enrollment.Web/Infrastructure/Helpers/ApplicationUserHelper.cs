using Enrollment.Web.Model;

namespace Enrollment.Web.Infrastructure.Helpers
{
    public static class ApplicationUserHelper
    {
        public static string GeneratePassword(ApplicationUser user, int length = 8)
        {
            return "abcd1234!";
            //return Membership.GeneratePassword(length, 1);
        }
    }
}
