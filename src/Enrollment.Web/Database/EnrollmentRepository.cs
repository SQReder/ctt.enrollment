using Enrollment.DataAccess;
using Enrollment.Model;
using Enrollment.Web.Model;
using Microsoft.EntityFrameworkCore;

namespace Enrollment.Web.Database
{
    public class EnrollmentRepository : DBRepository<Enrollee>
    {
        public EnrollmentRepository(DbSet<Enrollee> set) : base(set)
        {
        }
    }
}
