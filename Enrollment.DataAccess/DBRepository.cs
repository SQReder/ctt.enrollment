using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.ChangeTracking;

namespace Enrollment.DataAccess
{
    public class DBRepository<T> : IRepository<T>
        where T : class
    {
        private readonly DbSet<T> _set;

        public DBRepository(DbSet<T> set)
        {
            _set = set;
        }

        public EntityEntry<T> Add(T entity)
        {
            return _set.Add(entity);
        }

        public void Attach(T entity)
        {
            _set.Attach(entity);
        }

        public void Remove(T entity)
        {
            _set.Remove(entity);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _set.AsEnumerable().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Expression Expression => _set.AsQueryable().Expression;

        public Type ElementType => _set.AsQueryable().ElementType;

        public IQueryProvider Provider => _set.AsQueryable().Provider;

        public IQueryable<T> AsNoTracking() => _set.AsNoTracking();

        public Task<T> FirstOrDefaultAsync(Expression<Func<T, Boolean>> predicate) => _set.FirstOrDefaultAsync(predicate);
    }
}