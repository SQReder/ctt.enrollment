using System;
using System.Threading.Tasks;
using AutoMapper;
using Enrollment.DataAccess;
using Enrollment.Model;
using Enrollment.Web.Database;
using Enrollment.Web.Infrastructure.Http.Responces;
using Enrollment.Web.Infrastructure.ViewModels;
using Enrollment.Web.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Enrollment.Web.Controllers
{
    public class EnrolleeController: Controller
    {
        private readonly EnrollmentDbContext _dataContext;

        public EnrolleeController(EnrollmentDbContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IActionResult Layout() => View();
        public IActionResult ListLayout() => View();
        public IActionResult EditLayout() => View();
        public IActionResult ViewLayout() => View();

        [HttpGet]
        public IActionResult ListEnrollee()
        {
            GenericResult result;
            try
            {
                var viewModels = new[]
                {
                    new EnrolleeViewModel
                    {
                        Id = new Guid("ff668380-3842-4b29-9672-5dc7e82d9905"),
                        FirstName = "Никита",
                        MiddleName = "Васильевич",
                        LastName = "Пупкин",
                        AddressSameAsParent = true,
                        RelationType = RelationTypeEnum.Child,
                    },
                };
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
                var repository = _dataContext.Repository<Enrollee>();
                var enrollee = repository.Add(new Enrollee
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Никита",
                    MiddleName = "Васильевич",
                    LastName = "Пупкин",
                    AddressSameAsParent = true,
                    RelationType = RelationTypeEnum.Child,
                });
                await _dataContext.SaveChangesAsync();
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
                var repository = _dataContext.EnrolleeRepository;
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
    }
}