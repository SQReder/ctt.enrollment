using System;
using System.Threading.Tasks;
using Enrollment.EntityFramework;
using Enrollment.Model;
using Microsoft.EntityFrameworkCore;

namespace Enrollment.DataAccess.Queries
{
    public class GetTrusteeByOwnerIDQuery
    {
        private readonly ApplicationDbContext _dbContext;

        public GetTrusteeByOwnerIDQuery(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Trustee> Execute(Guid ownerID)
        {
            var result = await _dbContext.Repository<Trustee>()
                .Include(trustee => trustee.Address)
                .FirstOrDefaultAsync(trustee => trustee.OwnerID == ownerID);
            return result;
        }
    }
}
