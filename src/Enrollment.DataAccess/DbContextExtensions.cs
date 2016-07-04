using Microsoft.EntityFrameworkCore;

namespace Enrollment.DataAccess
{
    public static class DbContextExtensions
    {
        public static IRepository<T> Repository<T>(this DbContext dbContext) 
            where T : class
        {
            return new DBRepository<T>(dbContext.Set<T>());
        }
    }
}
