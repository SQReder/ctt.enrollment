using System;
using System.Linq;
using System.Threading.Tasks;
using Enrollment.EntityFramework;
using Enrollment.Model;
using Microsoft.EntityFrameworkCore;

namespace Enrollment.DataAccess.Queries
{
    public static partial class AdmissionQueries
    {
        public class GetById
        {
            private readonly ApplicationDbContext _context;

            public GetById(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Admission> Execute(
                Guid id,
                bool withEnrollee,
                bool withUnity,
                bool withTrustee
            )
            {
                IQueryable<Admission> queryable = _context.Repository<Admission>();

                if (withEnrollee)
                {
                    queryable = queryable.Include(admission => admission.Enrollee);
                }

                if (withUnity)
                {
                    queryable = queryable.Include(admission => admission.Unity);
                }

                if (withTrustee)
                {
                    queryable = queryable.Include(admission => admission.Trustee);
                }

                var result = await queryable.FirstOrDefaultAsync(admission => admission.Id == id);

                return result;
            }
        }
    }
}