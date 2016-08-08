using System;
using System.Collections.Generic;
using Enrollment.Model.Contracts;

namespace Enrollment.Model
{
    // ReSharper disable once ClassWithVirtualMembersNeverInherited.Global
    public class Enrollee: IHaveIdentifier, IHaveAlternateIdentifier
    {
        public Guid Id { get; set; }
        public int AlternateId { get; set; }

        public RelationTypeEnum RelationType { get; set; }

        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        /// <summary>
        /// Адрес
        /// </summary>
        public virtual Address Address { get; set; }

        /// <summary>
        /// Адрес такой-же как у попечителя (родителя/родственника)
        /// </summary>
        public bool AddressSameAsParent { get; set; }

        /// <summary>
        /// Школа
        /// </summary>
        public string StudyPlaceTitle { get; set; }

        /// <summary>
        /// Класс
        /// </summary>
        public string StudyGrade { get; set; }

        /// <summary>
        /// Id свидетельства о рождении
        /// </summary>
        public Guid BirthCertificateId { get; set; } // optional

        /// <summary>
        /// Попечитель (родитель/родственник)
        /// </summary>
        public virtual Trustee Parent { get; set; }

        public virtual ICollection<Admission> Admissions { get; set; }
    }
}