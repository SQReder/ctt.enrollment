using Enrollment.EntityFramework.Identity.Model;

namespace Enrollment.EntityFramework.Identity.Helpers
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
