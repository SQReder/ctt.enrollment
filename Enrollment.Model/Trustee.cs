using System;
using System.Collections.Generic;
using Enrollment.Model.Contracts;

namespace Enrollment.Model
{
    public enum Sex: byte
    {
        Female = 0,
        Male = 1
    }

// ReSharper disable once ClassWithVirtualMembersNeverInherited.Global
    public class Trustee: IHaveIdentifier, IHaveAlternateIdentifier
    {
        public Guid Id { get; set; }
        public int AlternateId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }

        public Sex Sex { get; set; }

        public string Job { get; set; }
        public string JobPosition { get; set; }

        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public Address Address { get; set; }

        /// <summary>
        /// Reference to owner user acount id
        /// </summary>
        public Guid? OwnerID { get; set; }

        public virtual ApplicationUser Owner { get; set; }

        public virtual ICollection<Enrollee> Enrollees { get; set; }

        public virtual ICollection<Admission> Admissions { get; set; }
    }
}