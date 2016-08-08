using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Enrollment.Web.Database;
using Microsoft.AspNetCore.Mvc;
using Enrollment.DataAccess;
using Enrollment.Model;
using Enrollment.Web.Infrastructure.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Enrollment.Web.Controllers.WebApi
{
    [Route("api/[controller]")]
    public class TrusteesController : BaseController
    {
        private readonly ILogger _logger;

        public TrusteesController(
            ApplicationDbContext dbContext,
            UserManager<ApplicationUser> userManager,
            ILogger<TrusteesController> logger
            ) : base(dbContext, userManager)
        {
            _logger = logger;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<TrusteeViewModel> ListTrustees()
        {
            var repository = DataContext.Repository<Trustee>();

            var query = repository.AsNoTracking();

            var trustees = query.ToList();

            var viewModels = Mapper.Map<TrusteeViewModel[]>(trustees);

            return viewModels;
        }


        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
