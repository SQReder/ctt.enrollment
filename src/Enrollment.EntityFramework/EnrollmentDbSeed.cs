﻿using System;
using System.Linq;
using Enrollment.Model;

namespace Enrollment.EntityFramework
{
    public static class EnrollmentDbSeed
    {
        public static void EnsureSeedData(this EnrollmentDbContext context)
        {
            var addressGuid = new Guid("ff668380-3842-4b29-9672-5dc7e82d9901");
            if (!context.Addresses.Any())
            {
                context.Addresses.Add(new Address
                {
                    Id = addressGuid,
                    Raw = "Москва"
                });
                context.SaveChanges();
            }
            if (!context.Enrollees.Any())
            {
                context.Enrollees.Add(new Enrollee
                {
                    Id = new Guid("ff668380-3842-4b29-9672-5dc7e82d9905"),
                    FirstName = "Никита",
                    MiddleName = "Васильевич",
                    LastName = "Пупкин",
                    AddressSameAsParent = true,
                    RelationType = RelationTypeEnum.Child,
                    Address = context.Addresses.First(x => x.Id == addressGuid),
                    StudyGrade = "123?",
                });

                context.SaveChanges();
            }
        }
    }
}