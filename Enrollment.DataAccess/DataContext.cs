using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Entity;

namespace Enrollment.DataAccess
{
    abstract public class DataContext : DbContext, IDataContext
    {
        private readonly Dictionary<Type, IQueryable> _repositories = new Dictionary<Type, IQueryable>();

        public IRepository<T> Repository<T>() where T : class
        {
            IQueryable repository;
            IRepository<T> result;

            if (_repositories.TryGetValue(typeof(T), out repository))
            {
                result = (IRepository<T>)repository;
            }
            else
            {
                result = new DBRepository<T>(Set<T>());

                _repositories[typeof(T)] = result;
            }

            return result;
        }

        public void MarkAsChanged<T>(T entity) where T : class
        {
            Entry(entity).State = EntityState.Modified;
        }
    }
}