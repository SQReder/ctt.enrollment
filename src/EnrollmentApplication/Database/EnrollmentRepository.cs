using Enrollment.DataAccess;
using EnrollmentApplication.Model;
using Microsoft.EntityFrameworkCore;

namespace EnrollmentApplication.Database
{
    public class EnrollmentRepository : DBRepository<Enrollee>
    {
        public EnrollmentRepository(DbSet<Enrollee> set) : base(set)
        {
        }
    }
}
