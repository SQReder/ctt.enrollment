using System;
using System.ComponentModel.DataAnnotations;

namespace Enrollment.Web.Infrastructure.ViewModels
{
    public class AdmissionViewModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public int AlternateId { get; set; }

        public Guid EnrolleeId { get; set; }
        public EnrolleeViewModel Enrollee { get; set; }

        public Guid UnityId { get; set; }
        public UnityViewModel Unity { get; set; }

    }
}