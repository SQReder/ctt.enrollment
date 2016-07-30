using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Enrollment.Model;

namespace Enrollment.Web.Infrastructure.ViewModels
{
    public class EnrolleeViewModel
    {
        public Guid? Id { get; set; }

        [Required]
        public RelationTypeEnum RelationType { get; set; }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string MiddleName { get; set; }
        [Required]
        public string LastName { get; set; }

        [Required]
        public AddressViewModel Address { get; set; }
        public bool AddressSameAsParent { get; set; }

        [Required]
        public string StudyPlaceTitle { get; set; }
        [Required]
        public string StudyGrade { get; set; }

        public Guid BirthCertificateId { get; set; }
    }
}