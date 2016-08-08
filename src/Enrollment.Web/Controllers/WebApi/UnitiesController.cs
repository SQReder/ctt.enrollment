using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Enrollment.DataAccess;
using Enrollment.Model;
using Enrollment.Web.Database;
using Enrollment.Web.Infrastructure.ViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Enrollment.Web.Controllers.WebApi
{
    [Route("api/[controller]")]
    public class UnitiesController : Controller
    {
        private readonly IRepository<Unity> _repository;

        public UnitiesController(ApplicationDbContext dbContext)
        {
            _repository = dbContext.Repository<Unity>();
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<UnityViewModel> Get()
        {
            var query = _repository.AsNoTracking();

            var unities = query.ToList();

            var viewModels = Mapper.Map<UnityViewModel[]>(unities);

            return viewModels;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]Unity value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(Guid id, [FromBody]Unity value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
        }
    }
}
