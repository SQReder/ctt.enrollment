using System;
using System.Threading.Tasks;

namespace Enrollment.Api.Infrastructure.Identity
{
    public interface ICurrentUserHelper
    {
        string Name();
        string GuidString();
        Guid Guid();
        bool IsLoggedIn();
        bool IsTrustee();
        bool IsAdmin();
    }
}