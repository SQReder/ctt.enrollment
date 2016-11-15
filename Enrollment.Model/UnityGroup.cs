using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Enrollment.Model
{
    public class UnityGroup
    {
        public Guid Id { get; set; }

        public virtual List<Unity> Unities { get; set; }

        [MaxLength(100)]
        public string Title { get; set; }
    }
}
