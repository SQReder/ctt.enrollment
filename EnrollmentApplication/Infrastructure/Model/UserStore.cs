using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace EnrollmentApplication.Infrastructure.Model
{
    //public class UserStore : IUserStore<ApplicationUser>, IUserPhoneNumberStore<ApplicationUser>,
    //    IUserPasswordStore<ApplicationUser>, IUserTwoFactorStore<ApplicationUser>, IUserLoginStore<ApplicationUser>
    //{
    //    private static readonly ApplicationUser AdminUser = new ApplicationUser
    //    {
    //        UserName = "a@a.a",
    //        PasswordHash = "123"
    //    };

    //    public Task SetPasswordHashAsync(ApplicationUser user, string passwordHash,
    //        CancellationToken cancellationToken)
    //    {
    //        user.PasswordHash = passwordHash;

    //        return Task.FromResult(0);
    //    }

    //    public Task<string> GetPasswordHashAsync(ApplicationUser user, CancellationToken cancellationToken)
    //    {
    //        return Task.FromResult(user.PasswordHash);
    //    }

    //    public Task<bool> HasPasswordAsync(ApplicationUser user, CancellationToken cancellationToken)
    //    {
    //        return Task.FromResult(user.PasswordHash != null);
    //    }

    //    public Task SetPhoneNumberAsync(ApplicationUser user, string phoneNumber, CancellationToken cancellationToken)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<string> GetPhoneNumberAsync(ApplicationUser user, CancellationToken cancellationToken)
    //    {
    //        return Task.FromResult(string.Empty);
    //    }

    //    public Task<bool> GetPhoneNumberConfirmedAsync(ApplicationUser user, CancellationToken cancellationToken)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task SetPhoneNumberConfirmedAsync(ApplicationUser user, bool confirmed,
    //        CancellationToken cancellationToken)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void Dispose()
    //    {
    //    }

    //    public Task<string> GetUserIdAsync(ApplicationUser user, CancellationToken cancellationToken)
    //    {
    //        return Task.FromResult(user.UserName);
    //    }

    //    public Task<string> GetUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
    //    {
    //        return Task.FromResult(user.UserName);
    //    }

    //    public Task SetUserNameAsync(ApplicationUser user, string userName, CancellationToken cancellationToken)
    //    {
    //        user.UserName = userName;

    //        return Task.FromResult(0);
    //    }

    //    public Task<string> GetNormalizedUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
    //    {
    //        return Task.FromResult(user.UserName);
    //    }

    //    public Task SetNormalizedUserNameAsync(ApplicationUser user, string normalizedName,
    //        CancellationToken cancellationToken)
    //    {
    //        user.UserName = normalizedName;

    //        return Task.FromResult(0);
    //    }

    //    public Task<IdentityResult> CreateAsync(ApplicationUser user, CancellationToken cancellationToken)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<IdentityResult> UpdateAsync(ApplicationUser user, CancellationToken cancellationToken)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<IdentityResult> DeleteAsync(ApplicationUser user, CancellationToken cancellationToken)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<ApplicationUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
    //    {
    //        ApplicationUser result;

    //        result = string.Equals(AdminUser.UserName, userId, StringComparison.CurrentCultureIgnoreCase)
    //            ? AdminUser
    //            : null;

    //        return Task.FromResult(result);
    //    }

    //    public Task<ApplicationUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
    //    {
    //        ApplicationUser result;

    //        result = string.Equals(AdminUser.UserName, normalizedUserName, StringComparison.CurrentCultureIgnoreCase)
    //            ? AdminUser
    //            : null;

    //        return Task.FromResult(result);
    //    }

    //    public Task SetTwoFactorEnabledAsync(ApplicationUser user, bool enabled, CancellationToken cancellationToken)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<bool> GetTwoFactorEnabledAsync(ApplicationUser user, CancellationToken cancellationToken)
    //    {
    //        return Task.FromResult(false);
    //    }

    //    public Task AddLoginAsync(ApplicationUser user, UserLoginInfo login, CancellationToken cancellationToken)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task RemoveLoginAsync(ApplicationUser user, string loginProvider, string providerKey,
    //        CancellationToken cancellationToken)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<IList<UserLoginInfo>> GetLoginsAsync(ApplicationUser user, CancellationToken cancellationToken)
    //    {
    //        return Task.FromResult((IList<UserLoginInfo>)new List<UserLoginInfo>());
    //    }

    //    public Task<ApplicationUser> FindByLoginAsync(string loginProvider, string providerKey, CancellationToken cancellationToken)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}