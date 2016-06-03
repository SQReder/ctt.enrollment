using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;

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
