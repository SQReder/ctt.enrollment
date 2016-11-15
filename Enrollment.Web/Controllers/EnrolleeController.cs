using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Enrollment.DataAccess;
using Enrollment.EntityFramework;
using Enrollment.Model;
using Enrollment.Web.Infrastructure.Exceptions;
using Enrollment.Web.Infrastructure.Http.Responces;
using Enrollment.Web.Infrastructure.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
                var userId = GetCurrentUserId();

                var trustee = await DataContext
                    .Repository<Trustee>()
                    .AsNoTracking()
                    .Include(x => x.Enrollees)
                    .FirstOrDefaultAsync(x => x.OwnerID == userId);

                var viewModels = Mapper.Map<EnrolleeViewModel[]>(trustee.Enrollees);

                result = new GetEnrolleesResult(viewModels);
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

                if (enrollee != null)
                {
                    result = new GetEnrolleeResult(enrollee);
                }
                else
                {
                    throw new NotFoundException("Enrollee not found");
                }
            }
            catch (Exception e)
            {
                result = new ErrorResult(e);
            }
            return new ObjectResult(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EnrolleeViewModel model)
        {
            return new ObjectResult(new ErrorResult(new NotImplementedException()));
            //GenericResult result;

            //try
            //{
            //    if (!ModelState.IsValid)
            //    {
            //        throw new ValidationException();
            //    }

            //    var repository = DataContext.EnrolleeRepository;

            //    Enrollee enrollee;

            //    var id = model.Id;
            //    if (id.HasValue)
            //    {
            //        enrollee = repository
            //            .AsQueryable()
            //            .Include(x => x.Address)
            //            .FirstOrDefault(x => x.Id == id.Value);

            //        Mapper.Map(model, enrollee);
            //    }
            //    else
            //    {
            //        var userID = GetCurrentUserId();
            //        var trustee = DataContext
            //            .Repository<Trustee>()
            //            .Include(x => x.Applicants)
            //            .First(x => x.OwnerID == userID);

            //        enrollee = Mapper.Map<Enrollee>(model);
            //        enrollee.Id = Guid.NewGuid();

            //        trustee.Applicants.Add(enrollee);

            //        //repository.Add(enrollee);
            //    }


            //    await DataContext.SaveChangesAsync();

            //    result = new GuidResult(enrollee.Id);
            //}
            //catch (Exception e)
            //{
            //    result = new ErrorResult(e);
            //}

            //return new ObjectResult(result);
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
                    .Include(x => x.Enrollees)
                    .First(x => x.OwnerID == userID);


                var toDelete = trustee.Enrollees.FirstOrDefault(x => x.Id == id);

                if (toDelete != null)
                {
                    trustee.Enrollees.Remove(toDelete);
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