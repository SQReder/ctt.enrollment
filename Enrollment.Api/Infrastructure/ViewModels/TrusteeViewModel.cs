using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Enrollment.Model;

namespace Enrollment.Api.Infrastructure.ViewModels
{
    public class TrusteeViewModel
    {
        public Guid Id { get; set; }

        [MaxLength(100)]
        public string FirstName { get; set; }

        [MaxLength(100)]
        public string LastName { get; set; }

        [MaxLength(100)]
        public string MiddleName { get; set; }

        public Sex Sex { get; set; }

        [MaxLength(200)]
        public string Job { get; set; }

        [MaxLength(100)]
        public string JobPosition { get; set; }

        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public IEnumerable<Guid> Enrollees { get; set; }

        public IEnumerable<Guid> Admissions { get; set; }
    }
}