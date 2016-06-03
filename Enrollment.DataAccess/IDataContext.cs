using System;

namespace Enrollment.DataAccess
{
    public interface IDataContext
    {
        IRepository<T> Repository<T>() where T : class;
        Int32 SaveChanges();
        void MarkAsChanged<T>(T entity) where T : class;
    }
}