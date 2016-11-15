using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Enrollment.Api.Infrastructure;
using Enrollment.Api.Infrastructure.Http.Responces;
using Enrollment.Api.Infrastructure.Identity;
using Enrollment.Api.Infrastructure.ViewModels;
using Enrollment.DataAccess;
using Enrollment.DataAccess.Queries;
using Enrollment.EntityFramework;
using Enrollment.Model;
using Enrollment.ServiceLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;

namespace Enrollment.Api.Controllers
{
    [Authorize(Roles = "Trustee,Manager,Admin")]
    [Route("api/trustee")]
    public class TrusteeController : CrudControllerBase<TrusteeViewModel>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IUserManagerHelper _userManagerHelper;
        private readonly IRandomIdGenerator _randomIdGenerator;
        private readonly ILogger<TrusteeController> _logger;
        private readonly IMapper _mapper;
        private ICurrentUserHelper _currentUserHelper;

        public TrusteeController(
            ApplicationDbContext dbContext,
            IUserManagerHelper userManagerHelper,
            IRandomIdGenerator randomIdGenerator,
            ILogger<TrusteeController> logger,
            Func<ControllerBase, ICurrentUserHelper> currentUserHelperFactory,
            AutoMapper.IConfigurationProvider mappingConfigurationProvider
        )
            : base(logger)
        {
            _dbContext = dbContext;
            _userManagerHelper = userManagerHelper;
            _randomIdGenerator = randomIdGenerator;
            _logger = logger;
            _mapper = mappingConfigurationProvider.CreateMapper();
            _currentUserHelper = currentUserHelperFactory(this);
        }

        protected override async Task<Guid> CreateInternal(TrusteeViewModel model)
        {
            var user = await _userManagerHelper.FindByIdAsync(_currentUserHelper.GuidString());

            var trustee = CreateTrusteeForUser(model, user);

            _dbContext.Repository<Trustee>().Add(trustee);

            await _dbContext.SaveChangesAsync();

            return trustee.Id;
        }

        protected override async Task<TrusteeViewModel> ReadInternal(Guid id, ILookup<string, string> param)
        {
            var withAddress = false;
            var withEnrollees = false;
            var withAdmissions = false;

            var includes = param.FirstOrDefault(x => x.Key == "include")?.ToArray();

            if (includes != null)
            {
                withAddress = includes.Contains("address");
                withEnrollees = includes.Contains("enrollees");
                withAdmissions = includes.Contains("admissions");
            }

            var query = new GetTrusteeByIdQuery(_dbContext);

            var trustee = await query.Execute(id, withAddress, withEnrollees, withAdmissions);

            var viewModel = _mapper.Map<TrusteeViewModel>(trustee);

            return viewModel;
        }

        protected override async Task UpdateInternal(Guid id, TrusteeViewModel model)
        {
            var query = new GetTrusteeByOwnerIDQuery(_dbContext);

            var trustee = await query.Execute(_currentUserHelper.Guid());

            if (trustee != null && trustee.Id == id)
            {
                _mapper.Map(model, trustee);

                await _dbContext.SaveChangesAsync();
            }
        }

        protected override async Task DeleteInternal(Guid id)
        {
            throw new NotImplementedException();
        }


        private Trustee CreateTrusteeForUser(TrusteeViewModel model, ApplicationUser user)
        {
            var trustee = _mapper.Map<Trustee>(model);

            trustee.Id = Guid.NewGuid();
            trustee.Owner = user;
            trustee.AlternateId = _randomIdGenerator.Generate();

            return trustee;
        }

        #region Overrides of CrudControllerBase<TrusteeViewModel>

        protected override Task<TrusteeViewModel[]> ListInternal(ILookup<string, string> param)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}