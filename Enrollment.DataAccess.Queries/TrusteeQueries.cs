using System;
using System.Threading.Tasks;
using Enrollment.EntityFramework;
using Enrollment.Model;
using Microsoft.EntityFrameworkCore;

namespace Enrollment.DataAccess.Queries
{
    public static partial class TrusteeQueries
    {
        public class GetByOwnerId
        {
            private readonly ApplicationDbContext _context;

            public GetByOwnerId(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Trustee> Execute(Guid id)
            {
                var result = await _context
                    .Repository<Trustee>()
                    .Include(trustee => trustee.Enrollees)
                    .Include(trustee => trustee.Admissions)
                    .FirstOrDefaultAsync(trustee => trustee.OwnerID == id);

                return result;
            }
        }
    }
}