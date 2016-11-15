using System.Linq;
using System.Threading.Tasks;
using Enrollment.EntityFramework;
using Enrollment.Model;
using Microsoft.EntityFrameworkCore;

namespace Enrollment.DataAccess.Queries
{
    public static partial class UnityGroupQueries
    {
        public class ListUnityGroups
        {
            private readonly ApplicationDbContext _context;

            public ListUnityGroups(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<UnityGroup[]> Execute(bool withUnities)
            {
                IQueryable<UnityGroup> queryable = _context.Repository<UnityGroup>();

                if (withUnities)
                {
                    queryable = queryable.Include(x => x.Unities);
                }

                var unityGroups = await queryable.ToArrayAsync();

                return unityGroups;
            }
        }
    }
}