using System;
using System.Threading.Tasks;
using Enrollment.EntityFramework;
using Enrollment.Model;

namespace Enrollment.DataAccess.Queries
{
    public static partial class EnrolleeQueries
    {
        public class RemoveById
        {
            private readonly ApplicationDbContext _context;

            public RemoveById(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task Execute(Guid id)
            {
                var repository = _context.Repository<Enrollee>();

                var enrollee = await repository.FirstOrDefaultAsync(x => x.Id == id);

                if (enrollee != null)
                    repository.Remove(enrollee);
            }
        }
    }
}