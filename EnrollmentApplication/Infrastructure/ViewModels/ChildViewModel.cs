using System;
using Enrollment.Model;

namespace EnrollmentApplication.Infrastructure.ViewModels
{
    public class ChildViewModel
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

        public Guid BirthCertificateId { get; set; }

        public static ChildViewModel Create(Enrollee child)
        {
            return new ChildViewModel
            {
                Id = child.Id,
                RelationshipDegree = child.RelationshipDegree,
                FirstName = child.FirstName,
                MiddleName = child.MiddleName,
                LastName = child.LastName,
                Address = child.Address,
                AddressSameAsParent = child.AddressSameAsParent,
                StudyPlaceTitle = child.StudyPlaceTitle,
                StudyGrade = child.StudyGrade,
                BirthCertificateId = child.BirthCertificateId,
            };
                            
        }
    }
}