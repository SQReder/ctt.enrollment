using System;
using System.Collections.Generic;
using System.Linq;
using Enrollment.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Enrollment.EntityFramework
{
    public static class ApplicationDbSeed
    {
        private static void EnsureSeedData(this ApplicationDbContext context)
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
                context.SaveChanges();
            }

            if (!context.UnityGroups.Any())
            {
                var unityGroup = new UnityGroup
                {
                    Id = Guid.NewGuid(),
                    Title = "Конструкторское бюро",
                    Unities = new List<Unity>
                    {
                        new Unity {Id = Guid.NewGuid(), Title = "Лего-конструирование и моделирование"},
                        new Unity {Id = Guid.NewGuid(), Title = "Инженерное 3D-моделирование и конструирование"}
                    }
                };

                context.UnityGroups.Add(unityGroup);

                context.SaveChanges();
            }
        }

        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                EnsureSeedData(context);
            }
        }
    }
}