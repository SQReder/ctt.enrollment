using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Enrollment.Model
{
    public class ApplicationUser: IdentityUser<Guid>
    {
        public virtual Trustee Trustee { get; set; }
    }
}