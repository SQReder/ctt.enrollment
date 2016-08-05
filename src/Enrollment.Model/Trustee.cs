using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Enrollment.Model
{
    public class Trustee
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }

        public string Job { get; set; }
        public string JobPosition { get; set; }

        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public Address Address { get; set; }

        public virtual ICollection<Enrollee> Applicants { get; set; }

        public Guid? OwnerID { get; set; }

        public virtual ApplicationUser Owner { get; set; }
    }
}