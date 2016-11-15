using System;
using Enrollment.Api.Infrastructure.Http.Responces;
using Enrollment.Model;

namespace Enrollment.Api.Infrastructure.ViewModels
{
    public class ProfileViewModel
    {
        public Guid Id { get; set; }
        public Guid TrusteeId { get; set; }
        public string Email { get; set; }

        public ProfileViewModel(ApplicationUser user, Trustee trustee)
        {
            Id = user.Id;
            TrusteeId = trustee.Id;
            Email = user.Email;            
        }
    }
}