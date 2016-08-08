using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Enrollment.Model
{
    public class Unity
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [MaxLength(100)]
        public string Title { get; set; }

        public virtual UnityGroup UnityGroup { get; set; }
        public virtual ICollection<Admission> Admissions { get; set; }
    }
}
