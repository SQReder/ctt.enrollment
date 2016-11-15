using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Enrollment.Api.Infrastructure.Exceptions;
using Enrollment.Api.Infrastructure.Identity;
using Enrollment.Api.Infrastructure.ViewModels;
using Enrollment.DataAccess.Queries;
using Enrollment.EntityFramework;
using Enrollment.Model;
using Enrollment.ServiceLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Enrollment.Api.Controllers
{
    [Authorize]
    [Route("api/enrollee")]
    public class EnrolleeController : CrudControllerBase<EnrolleeViewModel>
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<EnrolleeController> _logger;
        private readonly IRandomIdGenerator _randomIdGenerator;
        private readonly ICurrentUserHelper _currentUserHelper;
        private IMapper _mapper;

        public EnrolleeController(
            ApplicationDbContext context,
            ILogger<EnrolleeController> logger,
            Func<ControllerBase, ICurrentUserHelper> currentUserHelperFactory,
            AutoMapper.IConfigurationProvider mappingConfigurationProvider,
            IRandomIdGenerator randomIdGenerator
            )
            : base(logger)
        {
            _context = context;
            _logger = logger;
            _randomIdGenerator = randomIdGenerator;
            _currentUserHelper = currentUserHelperFactory(this);
            _mapper = mappingConfigurationProvider.CreateMapper();
        }

        #region Overrides of CrudControllerBase<EnrolleeViewModel>

        protected override async Task<Guid> CreateInternal(EnrolleeViewModel model)
        {
            if (!ModelState.IsValid)
                throw new ValidationException();

            var isEnrolleeExistsQuery = new EnrolleeQueries.IsEnrolleeExists(_context);

            var isEnrolleeExists = await isEnrolleeExistsQuery.Execute(model.Id);

            if (isEnrolleeExists)
            {
                throw new AlreadyExistsException(model.Id, model.AlternateId);
            }

            var trusteeQuery = new TrusteeQueries.GetByOwnerId(_context);

            var userId = _currentUserHelper.Guid();

            var trustee = await trusteeQuery.Execute(userId);

            if (trustee == null)
            {
                throw new NotFoundException("Trustee not found");
            }

            var enrollee = CreateEnrolleeForTrustee(model, trustee);

            trustee.Enrollees.Add(enrollee);

            await _context.SaveChangesAsync();

            return enrollee.Id;

        }

        private Enrollee CreateEnrolleeForTrustee(EnrolleeViewModel model, Trustee trustee)
        {
            var enrollee = _mapper.Map<Enrollee>(model);

            enrollee.Id = Guid.NewGuid();

            enrollee.AlternateId = _randomIdGenerator.Generate();
            enrollee.Trustee = trustee;

            return enrollee;
        }

        protected override async Task<EnrolleeViewModel> ReadInternal(Guid id, ILookup<string, string> param)
        {
            var enumerable = param.FirstOrDefault(x =>x.Key == "include") ?? Enumerable.Empty<string>();
            var includes = enumerable.ToArray();

            var withAddress = includes.Contains("address");

            var query = new EnrolleeQueries.GetById(_context);

            var enrollee = await query.Execute(id, withAddress);

            if (enrollee == null)
            {
                throw new NotFoundException();
            }

            var viewModel = _mapper.Map<EnrolleeViewModel>(enrollee);

            return viewModel;
        }

        protected override async Task UpdateInternal(Guid id, EnrolleeViewModel model)
        {
            var query = new EnrolleeQueries.GetById(_context);

            var enrollee = await query.Execute(id, false);

            if (enrollee == null)
            {
                throw new NotFoundException();
            }

            _mapper.Map(model, enrollee);

            await _context.SaveChangesAsync();
        }

        protected override async Task DeleteInternal(Guid id)
        {
            var query = new EnrolleeQueries.RemoveById(_context);

            await query.Execute(id);

            await _context.SaveChangesAsync();
        }

        protected override async Task<EnrolleeViewModel[]> ListInternal(ILookup<string, string> param)
        {
            var idStrings = param
                .FirstOrDefault(grouping => grouping.Key == "trustee") 
                ?? Enumerable.Empty<string>();

            var trusteeIds = idStrings
                .Select(Guid.Parse)
                .ToArray();

            var query = new EnrolleeQueries.ListEnrollees(_context);

            var enrollees = new List<Enrollee>();

            foreach (var trusteeId in trusteeIds)
            {
                var found = await query.Execute(trusteeId);

                enrollees.AddRange(found);
            }

            var viewModels = enrollees
                .Select(enrollee => _mapper.Map<EnrolleeViewModel>(enrollee))
                .ToArray();

            return viewModels;
        }

        #endregion
    }
}
