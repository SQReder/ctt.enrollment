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
        /// �����
        /// </summary>
        public virtual Address Address { get; set; }

        /// <summary>
        /// ����� �����-�� ��� � ���������� (��������/������������)
        /// </summary>
        public bool AddressSameAsParent { get; set; }

        /// <summary>
        /// �����
        /// </summary>
        public string StudyPlaceTitle { get; set; }

        /// <summary>
        /// �����
        /// </summary>
        public string StudyGrade { get; set; }

        /// <summary>
        /// Id ������������� � ��������
        /// </summary>
        public Guid BirthCertificateId { get; set; } // optional

        /// <summary>
        /// ���������� (��������/�����������)
        /// </summary>
        public virtual Trustee Parent { get; set; }

        public virtual ICollection<Admission> Admissions { get; set; }
    }
}