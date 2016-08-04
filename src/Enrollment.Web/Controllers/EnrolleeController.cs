using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Enrollment.DataAccess;
using Enrollment.Model;
using Enrollment.Web.Database;
using Enrollment.Web.Infrastructure.Http.Responces;
using Enrollment.Web.Infrastructure.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Semantics;
using Microsoft.EntityFrameworkCore;

namespace Enrollment.Web.Controllers
{
    public class EnrolleeController : BaseController
    {
        public EnrolleeController(ApplicationDbContext dataContext, UserManager<ApplicationUser> userManager)
            : base(dataContext, userManager)
        {
        }

        public IActionResult Layout() => View();
        public IActionResult ListLayout() => View();
        public IActionResult EditLayout() => View();
        public IActionResult ViewLayout() => View();

        [HttpGet]
        public async Task<IActionResult> ListEnrollee()
        {
            GenericResult result;
            try
            {
                var user = await GetCurrentUserAsync();
                var trusteeGuid = user.Id;

                var repository = DataContext.Repository<Enrollee>();
                var trustee = await DataContext
                    .Repository<Trustee>()
                    .AsNoTracking()
                    .Include(x => x.Applicants)
                    .FirstOrDefaultAsync(x => x.OwnerID == user.Id);
                var viewModels = Mapper.Map<EnrolleeViewModel[]>(trustee.Applicants);
                result = new ListEnrolleesResult(viewModels);
            }
            catch (Exception e)
            {
                result = new ErrorResult(e);
            }
            return new ObjectResult(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddChild()
        {
            GenericResult result;
            try
            {
                var repository = DataContext.Repository<Enrollee>();
                var enrollee = repository.Add(new Enrollee
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Никита",
                    MiddleName = "Васильевич",
                    LastName = "Пупкин",
                    AddressSameAsParent = true,
                    RelationType = RelationTypeEnum.Child,
                });
                await DataContext.SaveChangesAsync();
                result = new SuccessResult();
            }
            catch (Exception e)
            {
                result = new ErrorResult(e);
            }

            return new ObjectResult(result);
        }


        public async Task<IActionResult> Get(Guid id)
        {
            GenericResult result;
            try
            {
                var repository = DataContext.EnrolleeRepository;
                var enrollee = await repository.AsNoTracking()
                    .Include<Enrollee, Address>(x => x.Address)
                    .FirstOrDefaultAsync(e => e.Id == id);

                EnrolleeViewModel viewModel = null;
                if (enrollee != null)
                    viewModel = Mapper.Map<EnrolleeViewModel>(enrollee);

                result = new GetEnrolleeResult
                {
                    Succeeded = true,
                    Enrollee = viewModel,
                };
            }
            catch (Exception e)
            {
                result = new ErrorResult(e);
            }
            return new ObjectResult(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EnrolleeViewModel model)
        {
            GenericResult result;

            try
            {
                if (!ModelState.IsValid)
                {
                    throw new ValidationException();
                }

                var repository = DataContext.EnrolleeRepository;

                Enrollee enrollee;

                var id = model.Id;
                if (id.HasValue)
                {
                    enrollee = repository
                        .AsQueryable()
                        .Include(x => x.Address)
                        .FirstOrDefault(x => x.Id == id.Value);

                    Mapper.Map(model, enrollee);
                }
                else
                {
                    var userID = GetCurrentUserId();
                    var trustee = DataContext
                        .Repository<Trustee>()
                        .Include(x => x.Applicants)
                        .First(x => x.OwnerID == userID);

                    enrollee = Mapper.Map<Enrollee>(model);
                    enrollee.Id = Guid.NewGuid();

                    trustee.Applicants.Add(enrollee);

                    //repository.Add(enrollee);
                }


                await DataContext.SaveChangesAsync();

                result = new GuidResult(enrollee.Id);
            }
            catch (Exception e)
            {
                result = new ErrorResult(e);
            }

            return new ObjectResult(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            GenericResult result;
                    
            try
            {
                var userID = GetCurrentUserId();
                var trustee = DataContext
                    .Repository<Trustee>()
                    .Include(x => x.Applicants)
                    .First(x => x.OwnerID == userID);


                var toDelete = trustee.Applicants.FirstOrDefault(x => x.Id == id);

                if (toDelete != null)
                {
                    trustee.Applicants.Remove(toDelete);
                }

                await DataContext.SaveChangesAsync();

                result = new SuccessResult();
            }
            catch (Exception e)
            {
                result = new ErrorResult(e);
                throw;
            }

            return new ObjectResult(result);
        }
    }
}