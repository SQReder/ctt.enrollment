using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Enrollment.Api.Infrastructure.ViewModels;
using Enrollment.DataAccess.Queries;
using Enrollment.EntityFramework;
using Enrollment.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Enrollment.Api.Controllers
{
    [Route("api/unityGroups")]
    public class UnityGroupController : CrudControllerBase<UnityGroupViewModel>
    {
        private readonly ILogger<UnityGroupController> _logger;
        private readonly ApplicationDbContext _context;
        private IMapper _mapper;

        public UnityGroupController(
            ILogger<UnityGroupController> logger,
            ApplicationDbContext context,
            IConfigurationProvider mapperConfigurationProvider
            ) : base(logger)
        {
            _logger = logger;
            _context = context;
            _mapper = mapperConfigurationProvider.CreateMapper();
        }

        #region Overrides of CrudControllerBase<UnityGroupViewModel>

        protected override Task<Guid> CreateInternal(UnityGroupViewModel model)
        {
            throw new NotImplementedException();
        }

        protected override Task<UnityGroupViewModel> ReadInternal(Guid id, ILookup<string, string> param)
        {
            throw new NotImplementedException();
        }

        protected override async Task<UnityGroupViewModel[]> ListInternal(ILookup<string, string> param)
        {
            var enumerable = param.FirstOrDefault(x => x.Key == "include") ?? Enumerable.Empty<string>();

            var includes = enumerable.ToArray();

            var query = new UnityGroupQueries.ListUnityGroups(_context);

            var withUnities = includes.Contains("unities");

            var unityGroups = await query.Execute(withUnities);

            var viewModels = _mapper.Map<UnityGroupViewModel[]>(unityGroups);

            return viewModels;
        }

        protected override Task UpdateInternal(Guid id, UnityGroupViewModel model)
        {
            throw new NotImplementedException();
        }

        protected override Task DeleteInternal(Guid id)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}