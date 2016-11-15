using System;
using System.Linq;
using System.Threading.Tasks;
using Enrollment.EntityFramework;
using Enrollment.Model;
using Microsoft.EntityFrameworkCore;

namespace Enrollment.DataAccess.Queries
{
    public static partial class EnrolleeQueries
    {
        public class ListEnrollees
        {
            private readonly ApplicationDbContext _context;

            public ListEnrollees(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Enrollee[]> Execute(
                Guid? parentTrusteeId = null
            )
            {
                IQueryable<Enrollee> queryable = _context.Repository<Enrollee>();

                if (parentTrusteeId.HasValue)
                {
                    queryable = queryable.Where(enrollee => enrollee.TrusteeId == parentTrusteeId.Value);
                }

                var enrollees = await queryable.ToArrayAsync();

                return enrollees;
            }
        }
    }
}