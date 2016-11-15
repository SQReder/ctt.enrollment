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
        public class GetById
        {
            private readonly ApplicationDbContext _context;

            public GetById(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Enrollee> Execute(Guid id, bool withAddress)
            {
                IQueryable<Enrollee> queryable = _context.Repository<Enrollee>();

                if (withAddress)
                {
                    queryable = queryable.Include(enrollee => enrollee.Address);
                }

                var result = await queryable.FirstOrDefaultAsync(enrollee => enrollee.Id == id);

                return result;
            }
        }
    }
}