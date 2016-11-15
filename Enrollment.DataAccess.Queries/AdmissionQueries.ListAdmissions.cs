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
        public class ListAdmissions
        {
            private readonly ApplicationDbContext _context;

            public ListAdmissions(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Admission[]> Execute(
                Guid[] trusteeIds)
            {
                IQueryable<Admission> queryable = _context.Repository<Admission>();

                if (trusteeIds.Any())
                {
                    queryable = queryable.Where(admission => trusteeIds.Contains(admission.TrusteeId));
                }

                var result = queryable.ToArray();

                return result;
            }
        }
    }
}