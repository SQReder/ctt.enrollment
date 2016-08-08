using System;
using Enrollment.Model.Contracts;

namespace Enrollment.Model
{
    public class Admission: IHaveIdentifier, IHaveAlternateIdentifier
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int AlternateId { get; set; }

        public DateTime Created { get; set; } = DateTime.UtcNow;

        public Guid UnityId { get; set; }
        public Guid EnrolleeId { get; set; }
        public Guid ParentId { get; set; }

        public virtual Unity Unity { get; set; }
        public virtual Enrollee Enrollee { get; set; }
        public virtual Trustee Parent { get; set; }
    }
}