using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Enrollment.DataAccess;
using Enrollment.Model;
using Enrollment.Web.Database;
using Enrollment.Web.Infrastructure.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Enrollment.Web.Controllers.WebApi
{
    [Route("api/[controller]")]
    public class UnityGroupsController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public UnityGroupsController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<UnityGroupViewModel> List()
        {
            var repository = _dbContext.Repository<UnityGroup>();

            var query = repository.AsNoTracking();

            var unityGroups = query.ToList();

            var viewModels = Mapper.Map<UnityGroupViewModel[]>(unityGroups);

            return viewModels;
        }

        // GET api/values/5
        [HttpGet("{unityGroupId}")]
        public UnityGroup Group(Guid unityGroupId)
        {
            var repository = _dbContext.Repository<UnityGroup>();

            var query = repository.AsNoTracking();

            return query.FirstOrDefault(x => x.Id == unityGroupId);
        }

        [HttpGet("{unityGroupId}/unities/{unityId}")]
        public UnityViewModel Unity(Guid unityGroupId, Guid unityId)
        {
            var repository = _dbContext.Repository<Unity>();

            var query = repository.AsNoTracking();

            var unity = query.FirstOrDefault(x => x.Id == unityId && x.UnityGroup.Id == unityGroupId);

            var viewModel = Mapper.Map<UnityViewModel>(unity);

            return viewModel;
        }

        [HttpGet("{unityGroupId}/unities")]
        public IEnumerable<UnityViewModel> GroupUnities(Guid unityGroupId)
        {
            var repository = _dbContext.Repository<Unity>();

            var query = repository.AsNoTracking();

            var unity = query
                .Where(x => x.UnityGroup.Id == unityGroupId)
                .ToList();

            var viewModels = Mapper.Map<UnityViewModel[]>(unity);

            return viewModels;
        }


        // POST api/values
        [HttpPost]
        public void Post([FromBody]UnityGroup value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(Guid id, [FromBody]UnityGroup value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
        }
    }
}