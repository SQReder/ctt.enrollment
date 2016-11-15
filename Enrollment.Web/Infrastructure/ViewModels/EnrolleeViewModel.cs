using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Enrollment.Model;

namespace Enrollment.Web.Infrastructure.ViewModels
{
    public class EnrolleeViewModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public int AlternateId { get; set; }

        [Required]
        public Sex Sex { get; set; }

        [Required]
        public RelationTypeEnum RelationType { get; set; }

        [Required]
        public string FirstName { get; set; }

        public string MiddleName { get; set; }
        [Required]
        public string LastName { get; set; }

        [Required]
        public string Address { get; set; }
        public bool AddressSameAsParent { get; set; }

        [Required]
        public string StudyPlaceTitle { get; set; }
        [Required]
        public string StudyGrade { get; set; }

        public Guid BirthCertificateId { get; set; }
    }
}