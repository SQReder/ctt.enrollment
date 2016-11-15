using System;
using System.Threading.Tasks;
using Enrollment.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Enrollment.DataAccess.Queries
{
    public static partial class EnrolleeQueries
    {
        public class IsEnrolleeExists
        {
            private readonly ApplicationDbContext _context;

            public IsEnrolleeExists(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<bool> Execute(Guid id)
            {
                var result = await _context
                    .Repository<Model.Enrollee>()
                    .AsNoTracking()
                    .AnyAsync(enrollee => enrollee.Id == id);
                return result;
            }
        }
    }
}