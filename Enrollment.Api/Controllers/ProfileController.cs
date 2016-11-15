using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Enrollment.Api.Infrastructure;
using Enrollment.Api.Infrastructure.Exceptions;
using Enrollment.Api.Infrastructure.Http.Responces;
using Enrollment.Api.Infrastructure.Identity;
using Enrollment.Api.Infrastructure.ViewModels;
using Enrollment.DataAccess.Queries;
using Enrollment.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Enrollment.Api.Controllers
{
    [Authorize]
    [Route("api/profile")]
    public class ProfileController : CrudControllerBase<ProfileViewModel>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IUserManagerHelper _userManagerHelper;
        private readonly ILogger<TrusteeController> _logger;
        private IMapper _mapper;

        public ProfileController(
            ApplicationDbContext dbContext,
            IUserManagerHelper userManagerHelper,
            ILogger<TrusteeController> logger,
            Func<ControllerBase, ICurrentUserHelper> currentUserHelperFactory,
            IConfigurationProvider mappingConfigurationProvider
        ) : base(logger)
        {
            _dbContext = dbContext;
            _userManagerHelper = userManagerHelper;
            _logger = logger;
            _mapper = mappingConfigurationProvider.CreateMapper();
        }


        protected override Task<Guid> CreateInternal(ProfileViewModel model)
        {
            throw new NotImplementedException();
        }

        protected override async Task<ProfileViewModel> ReadInternal(Guid id, ILookup<string, string> param)
        {
            var user = await _userManagerHelper.FindByIdAsync(id.ToString());

            if (user == null)
            {
                throw new NotFoundException($"User with id {id} not found");
            }

            var query = new GetTrusteeByOwnerIDQuery(_dbContext);

            var trustee = await query.Execute(user.Id);

            var viewModel = trustee != null ? new ProfileViewModel(user, trustee) : null;

            return viewModel;
        }

        protected override Task UpdateInternal(Guid id, ProfileViewModel model)
        {
            throw new NotImplementedException();
        }

        protected override Task DeleteInternal(Guid id)
        {
            throw new NotImplementedException();
        }

        #region Overrides of CrudControllerBase<ProfileViewModel>

        protected override Task<ProfileViewModel[]> ListInternal(ILookup<string, string> param)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
