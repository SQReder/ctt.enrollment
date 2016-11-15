using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Routing;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Enrollment.Web.Controllers.WebApi
{
    public class PagedResult<T>
    {
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public string PrevPageLink { get; set; }
        public string NextPageLink { get; set; }
        public T[] Results { get; set; }
    }
}
