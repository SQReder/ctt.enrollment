using Enrollment.DataAccess;
using Enrollment.Model;
using Microsoft.EntityFrameworkCore;

namespace Enrollment.EntityFramework
{
    public class EnrollmentRepository : DBRepository<Enrollee>
    {
        public EnrollmentRepository(DbSet<Enrollee> set) : base(set)
        {
        }
    }
}
