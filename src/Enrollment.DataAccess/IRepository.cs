using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Enrollment.DataAccess
{
    public interface IRepository<TEntity> : IQueryable<TEntity>
        where TEntity : class
    {
        EntityEntry<TEntity> Add(TEntity entity);
        void Attach(TEntity entity);
        void Remove(TEntity entity);
        IQueryable<TEntity> AsNoTracking();
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
    }
}