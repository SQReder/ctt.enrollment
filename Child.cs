using System;

namespace Enrollment.Model
{
    public class Enrollee
    {
        public Guid Id { get; set; }

        public RelationshipDegreeEnum RelationshipDegree { get; set; }

        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        public string Address { get; set; }
        public bool AddressSameAsParent { get; set; }

        public string StudyPlaceTitle { get; set; }
        public string StudyGrade { get; set; }

        public Guid BirthCertificateId { get; set; } // optional

        public virtual Thrustee Parent { get; set; }
    }
}