using System;
using System.Collections.Generic;
using Enrollment.Model;

namespace Enrollment.Web.Infrastructure.ViewModels
{
    public class EnrolleeViewModel
    {
        public Guid Id { get; set; }

        public RelationTypeEnum RelationType { get; set; }

        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        public AddressViewModel Address { get; set; }
        public bool AddressSameAsParent { get; set; }

        public string StudyPlaceTitle { get; set; }
        public string StudyGrade { get; set; }

        public Guid BirthCertificateId { get; set; }
    }
}