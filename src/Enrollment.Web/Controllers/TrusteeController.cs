using System;
using System.Linq;
using System.Threading.Tasks;
using Enrollment.DataAccess;
using Enrollment.Model;
using Enrollment.Web.Database;
using Enrollment.Web.Infrastructure.Http.Responces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Enrollment.Web.Controllers
{
    public class TrusteeController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRepository<Trustee> _repository;

        public TrusteeController(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _repository = _dbContext.Repository<Trustee>();
        }

        public async Task<IActionResult> GetCurrentTrustee()
        {
            GenericResult result;

            try
            {
                var user = await GetCurrentUserAsync();
                var trustee = _repository
                    .AsNoTracking()
                    .Include(x => x.Address)
                    .Include(x => x.Applicants)
                    .FirstOrDefault(x => x.OwnerID == user.Id);
                result = new GetTrusteeResult(trustee);
            }
            catch (Exception e)
            {
                result = new ErrorResult(e);
            }

            return new ObjectResult(result);
        }

        public async Task<IActionResult> GetCurrentTrusteeAddress()
        {
            GenericResult result;

            try
            {
                var user = await GetCurrentUserAsync();
                var address = _repository.AsNoTracking()
                    .Include(x => x.Address)
                    .Where(x => x.OwnerID == user.Id)
                    .Select(x => x.Address)
                    .FirstOrDefault();
                result = new GetTrusteeAddressResult(address);
            }
            catch (Exception e)
            {
                result = new ErrorResult(e);
            }

            return new ObjectResult(result);
        }

        public IActionResult Get(Guid id)
        {
            GenericResult result;

            try
            {
                var repository = _repository;
                var trustee = repository
                    .AsNoTracking()
                    .FirstOrDefault(x => x.Id == id);
                result = new GetTrusteeResult(trustee);
            }
            catch (Exception e)
            {
                result = new ErrorResult(e);
            }

            return new ObjectResult(result);
        }

        private async Task<ApplicationUser> GetCurrentUserAsync()
        {
            return await _userManager.FindByIdAsync(_userManager.GetUserId(HttpContext.User));
        }

    }
}
