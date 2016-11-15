using System;
using System.Linq;
using System.Threading.Tasks;
using Enrollment.EntityFramework;
using Enrollment.Model;
using Microsoft.EntityFrameworkCore;

namespace Enrollment.DataAccess.Queries
{
    public class GetTrusteeByIdQuery
    {
        private readonly ApplicationDbContext _dbContext;

        public GetTrusteeByIdQuery(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Trustee> Execute(
            Guid id, 
            bool withAddress = true, 
            bool withEnrollees = false,
            bool withAdmissions = false
            )
        {
            IQueryable<Trustee> query = _dbContext.Repository<Trustee>();

            if (withAddress)
                query = query.Include(trustee => trustee.Address);

            if (withEnrollees)
                query = query.Include(trustee => trustee.Enrollees);

            if (withAdmissions)
                query = query.Include(trustee => trustee.Admissions);

            var result = await query.FirstOrDefaultAsync(trustee => trustee.Id == id);

            return result;
        }
    }
}