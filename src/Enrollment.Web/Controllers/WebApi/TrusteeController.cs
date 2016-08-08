using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Enrollment.DataAccess;
using Enrollment.Model;
using Enrollment.Web.Database;
using Enrollment.Web.Infrastructure.Exceptions;
using Enrollment.Web.Infrastructure.Http.Responces;
using Enrollment.Web.Infrastructure.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace Enrollment.Web.Controllers.WebApi
{
    [Authorize]
    [Route("api/me")]
    public class TrusteeController : BaseController
    {
        private readonly ILogger<TrusteeController> _logger;

        public TrusteeController(
            ApplicationDbContext dbContext,
            UserManager<ApplicationUser> userManager,
            ILogger<TrusteeController> logger
            ) : base(dbContext, userManager)
        {
            _logger = logger;
        }

        // GET api/values/5
        [HttpGet()]
        public TrusteeViewModel GetMe()
        {
            var userId = GetCurrentUserId();

            var repository = DataContext.Repository<Trustee>();

            var query = repository.AsNoTracking();

            var trustee = query.FirstOrDefault(x => x.OwnerID == userId);

            var viewModel = Mapper.Map<TrusteeViewModel>(trustee);

            return viewModel;
        }

        [HttpGet("enrollees")]
        public IEnumerable<EnrolleeViewModel> ListMyEnrollee()
        {
            var userId = GetCurrentUserId();

            var trustee = DataContext
                .Repository<Trustee>()
                .FirstOrDefault(x => x.OwnerID == userId);

            _logger.LogDebug("Trustee id " + trustee.Id);

            var enrollees = DataContext
                .EnrolleeRepository
                .AsNoTracking()
                .Where(x => x.Parent.Id == trustee.Id)
                .ToList();

            var enrolleeViewModels = Mapper.Map<EnrolleeViewModel[]>(enrollees);

            return enrolleeViewModels;
        }

        [HttpGet("admissions")]
        public IEnumerable<AdmissionViewModel> ListMyAdmissions()
        {
            var query = DataContext.Repository<Admission>().AsNoTracking();

            var admissions = query
                .Include(x => x.Unity)
                .Include(x => x.Enrollee)
                .ToList();

            var viewModels = Mapper.Map<AdmissionViewModel[]>(admissions);

            return viewModels;
        }

        [HttpPost("admissions")]
        public async Task<GenericResult> CreateMyAdmission([FromBody] AdmissionViewModel model)
        {
            GenericResult result;

            try
            {
                var enrollee = DataContext.EnrolleeRepository.AsNoTracking().FirstOrDefault(x => x.Id == model.EnrolleeId);
                if (enrollee == null)
                {
                    throw new NotFoundException("Enrollee not found");
                }

                var unity = DataContext.Repository<Unity>().AsNoTracking().FirstOrDefault(x => x.Id == model.UnityId);
                if (unity == null)
                {
                    throw new NotFoundException("Unity not found");
                }

                var trustee = await GetTrusteeWithAdmisions();

                var admission = new Admission
                {
                    Id = model.Id,
                    AlternateId = model.AlternateId,
                    EnrolleeId = model.EnrolleeId,
                    UnityId = model.UnityId,
                    ParentId = trustee.Id
                };

                var entry = DataContext.Repository<Admission>().Add(admission);

                await DataContext.SaveChangesAsync();

                result = new SuccessResult();
            }
            catch (Exception e)
            {
                result = new ErrorResult(e);
            }

            return result;
        }

        private async Task<Trustee> GetTrusteeWithAdmisions()
        {
            var userId = GetCurrentUserId();

            var trustee = await DataContext.Repository<Trustee>()
                .Include(x => x.Admissions)
                .FirstOrDefaultAsync(x => x.OwnerID == userId);

            return trustee;
        }

    }
}
