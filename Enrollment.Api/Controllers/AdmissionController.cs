using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Enrollment.Api.Infrastructure.ViewModels;
using Enrollment.DataAccess.Queries;
using Enrollment.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Enrollment.Api.Controllers
{
    [Route("api/admission")]
    public class AdmissionController: CrudControllerBase<AdmissionViewModel>
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AdmissionController> _logger;
        private IMapper _mapper;

        public AdmissionController(
            ApplicationDbContext context,
            ILogger<AdmissionController> logger,
            IConfigurationProvider mappingConfigurationProvider
            ) : base(logger)
        {
            _context = context;
            _logger = logger;
            _mapper = mappingConfigurationProvider.CreateMapper();
        }

        #region Overrides of CrudControllerBase<AdmissionViewModel>

        protected override Task<Guid> CreateInternal(AdmissionViewModel model)
        {
            throw new NotImplementedException();
        }

        protected override Task<AdmissionViewModel> ReadInternal(Guid id, ILookup<string, string> param)
        {
            throw new NotImplementedException();
        }

        protected override async Task<AdmissionViewModel[]> ListInternal(ILookup<string, string> param)
        {
            var enumerable = param.FirstOrDefault(x =>x .Key == "trusteeId") ?? Enumerable.Empty<string>();

            var trusteeIds = enumerable.Select(Guid.Parse).ToArray();

            var query = new AdmissionQueries.ListAdmissions(_context);

            var admissions = await query.Execute(trusteeIds);

            var viewModels = _mapper.Map<AdmissionViewModel[]>(admissions);

            return viewModels;
        }

        protected override Task UpdateInternal(Guid id, AdmissionViewModel model)
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
