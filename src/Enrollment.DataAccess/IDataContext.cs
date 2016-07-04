namespace Enrollment.DataAccess
{
    public interface IDataContext
    {
        IRepository<T> Repository<T>() where T : class;
        int SaveChanges();
        void MarkAsChanged<T>(T entity) where T : class;
    }
}