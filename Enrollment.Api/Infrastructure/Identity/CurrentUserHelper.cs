using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Enrollment.Api.Controllers;
using Enrollment.Model;
using Microsoft.AspNetCore.Mvc;

namespace Enrollment.Api.Infrastructure.Identity
{
    public class CurrentUserHelper : ICurrentUserHelper
    {
        private readonly IUserManagerHelper _userManagerHelper;
        private readonly ControllerBase _baseController;

        public CurrentUserHelper(IUserManagerHelper userManagerHelper, ControllerBase baseController)
        {
            _userManagerHelper = userManagerHelper;
            _baseController = baseController;
        }

        public virtual string Name()
        {
            return HttpContext.Current.User.Identity.Name;
        }

        public virtual string GuidString()
        {
            return _userManagerHelper.GetUserId(_baseController.HttpContext.User);
        }

        public Guid Guid()
        {
            return System.Guid.Parse(GuidString());
        }

        public virtual bool IsLoggedIn()
        {
            return HttpContext.Current.User.Identity.IsAuthenticated;
        }

        public virtual bool IsTrustee()
        {
            return HttpContext.Current.User.IsInRole("Trustee");
        }

        public virtual bool IsAdmin()
        {
            return HttpContext.Current.User.IsInRole("Admin");
        }
    }
}
