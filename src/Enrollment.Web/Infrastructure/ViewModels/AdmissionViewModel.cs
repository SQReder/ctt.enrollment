using System;

namespace Enrollment.Web.Infrastructure.ViewModels
{
    public class AdmissionViewModel
    {
        public Guid Id { get; set; }

        public EnrolleeViewModel Enrollee { get; set; }
        public UnityViewModel Unity { get; set; }

        public int AlternateId { get; set; }
        public Guid UnityId { get; set; }
        public Guid EnrolleeId { get; set; }
    }
}