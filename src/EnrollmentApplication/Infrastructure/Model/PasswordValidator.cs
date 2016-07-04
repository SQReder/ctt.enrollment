namespace EnrollmentApplication.Infrastructure.Model
{
    //public class PasswordValidator : PasswordValidator<ApplicationUser>, IPasswordHasher<ApplicationUser>
    //{
    //    public new Task<IdentityResult> ValidateAsync(UserManager<ApplicationUser> manager, ApplicationUser user, string password)
    //    {
    //        var result = password == user.PasswordHash ? IdentityResult.Success : IdentityResult.Failed();

    //        return Task.FromResult(result);
    //    }

    //    public string HashPassword(ApplicationUser user, string password)
    //    {
    //        return password;
    //    }

    //    public PasswordVerificationResult VerifyHashedPassword(ApplicationUser user, string hashedPassword,
    //        string providedPassword)
    //    {
    //        var result = hashedPassword == providedPassword
    //            ? PasswordVerificationResult.Success
    //            : PasswordVerificationResult.Failed;

    //        return result;
    //    }
    //}
}