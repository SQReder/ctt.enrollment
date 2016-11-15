using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Enrollment.DataAccess;
using Enrollment.EntityFramework;
using Enrollment.Model;
using Enrollment.Web.Infrastructure.Exceptions;
using Enrollment.Web.Infrastructure.Http.Responces;
using Enrollment.Web.Infrastructure.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using SelectPdf;

namespace Enrollment.Web.Controllers.WebApi
{
    [Authorize()]
    [Route("api/me")]
    public class CurrentTrusteeController : BaseController
    {
        private readonly ILogger<CurrentTrusteeController> _logger;

        public CurrentTrusteeController(
            ApplicationDbContext dbContext,
            UserManager<ApplicationUser> userManager,
            ILogger<CurrentTrusteeController> logger
        ) : base(dbContext, userManager)
        {
            _logger = logger;
        }

        #region Trustee methods

        // GET api/values/5
        [HttpGet]
        public GenericResult GetMe()
        {
            GenericResult result;

            try
            {
                var userId = GetCurrentUserId();

                var repository = DataContext.Repository<Trustee>();

                var query = repository.AsNoTracking()
                    .Include(x => x.Address);

                var trustee = query
                    .FirstOrDefault(x => x.OwnerID == userId);

                result = new GetTrusteeResult(trustee);
            }
            catch (Exception e)
            {
                result = new ErrorResult(e);
            }
            return result;
        }

        #endregion

        #region Enrollees methods

        [HttpGet("enrollees")]
        public GenericResult GetEnrollees()
        {
            GenericResult result;

            try
            {
                var userId = GetCurrentUserId();

                var trustee = DataContext
                    .Repository<Trustee>()
                    .Include(x => x.Enrollees)
                    .FirstOrDefault(x => x.OwnerID == userId);

                result = new GetEnrolleesResult(trustee.Enrollees);
            }
            catch (Exception e)
            {
                result = new ErrorResult(e);
            }

            return result;
        }

        [HttpPost("enrollees")]
        public async Task<GenericResult> CreateEnrolle([FromBody] EnrolleeViewModel model)
        {
            GenericResult result;

            try
            {
                if (!ModelState.IsValid)
                    throw new ValidationException();

                var repository = DataContext.EnrolleeRepository;

                var found = await repository
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == model.Id || x.AlternateId == model.AlternateId);

                if (found != null)
                {
                    throw new EntityAlreadyExistsException(model.Id, model.AlternateId);
                }

                var enrollee = Mapper.Map<Enrollee>(model);

                var trustee = await DataContext.Trustees
                    .Include(x => x.Enrollees)
                    .FirstOrDefaultAsync(x => x.OwnerID == GetCurrentUserId());

                trustee.Enrollees.Add(enrollee);

                await DataContext.SaveChangesAsync();

                result = new GuidResult(enrollee.Id);
            }
            catch (Exception e)
            {
                result = new ErrorResult(e);
            }
            return result;
        }

        [HttpGet("enrollees/{id}")]
        public async Task<GenericResult> GetEnrollee(Guid id)
        {
            GenericResult result;

            try
            {
                var repository = DataContext.EnrolleeRepository;
                var enrollee = await repository
                    .AsNoTracking()
                    .Include(x => x.Address)
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (enrollee == null)
                    throw new NotFoundException();

                result = new GetEnrolleeResult(enrollee);
            }
            catch (Exception e)
            {
                result = new ErrorResult(e);
            }

            return result;
        }

        [HttpDelete("enrollees/{id}")]
        public async Task<GenericResult> DeleteEnrollee(Guid id)
        {
            GenericResult result;

            try
            {
                var userId = GetCurrentUserId();

                var trustee = await DataContext.Trustees
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.OwnerID == userId);

                var enrollee = await DataContext.Enrollees
                    .Where(x => x.TrusteeId == trustee.Id)
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync();

                if (enrollee == null)
                    throw new NotFoundException();

                DataContext.Enrollees.Remove(enrollee);

                await DataContext.SaveChangesAsync();

                result = new SuccessResult();
            }
            catch (Exception e)
            {
                result = new ErrorResult(e);
            }

            return result;
        }

        #endregion

        #region Admission methods

        [HttpGet("admissions")]
        public async Task<GenericResult> ListMyAdmissions()
        {
            GenericResult result;

            try
            {
                var userId = GetCurrentUserId();

                var trusteeID = await DataContext.Repository<Trustee>()                   
                    .AsNoTracking()
                    .Where(t => t.OwnerID == userId)
                    .Select(t => t.Id)
                    .FirstOrDefaultAsync();

                var admissions = await DataContext.Admissions
                    .AsNoTracking()
                    .Include(x => x.Enrollee)
                    .Include(x => x.Unity)
                    .Where(a => a.TrusteeId == trusteeID)
                    .ToListAsync();


                result = new GetAdmissionsResult(admissions);
            }
            catch (Exception e)
            {
                result = new ErrorResult(e);
            }
            return result;
        }

        [HttpPost("admissions")]
        public async Task<GenericResult> CreateAdmission([FromBody] AdmissionViewModel model)
        {
            GenericResult result;

            try
            {
                if (!ModelState.IsValid)
                    throw new ValidationException();

                var enrollee =
                    DataContext.EnrolleeRepository.AsNoTracking().FirstOrDefault(x => x.Id == model.EnrolleeId);
                if (enrollee == null)
                {
                    throw new NotFoundException("Enrollee not found");
                }

                var unity = DataContext.Repository<Unity>().AsNoTracking().FirstOrDefault(x => x.Id == model.UnityId);
                if (unity == null)
                {
                    throw new NotFoundException("Unity not found");
                }

                var userId = GetCurrentUserId();

                var trustee = await DataContext.Repository<Trustee>()
                    .Include(x => x.Admissions)
                    .FirstOrDefaultAsync(x => x.OwnerID == userId);

                var admission = Mapper.Map<Admission>(model);

                trustee.Admissions.Add(admission);

                await DataContext.SaveChangesAsync();

                result = new SuccessResult();
            }
            catch (Exception e)
            {
                result = new ErrorResult(e);
            }

            return result;
        }

        [HttpDelete("admissions/{id}")]
        public async Task<GenericResult> DeleteAdmission(Guid id)
        {
            GenericResult result;

            try
            {
                var userId = GetCurrentUserId();

                var trusteeId =
                    await DataContext.Trustees.AsNoTracking()
                        .Where(x => x.OwnerID == userId)
                        .Select(x => x.Id)
                        .FirstOrDefaultAsync();

                var admission = await DataContext.Admissions
                    .Where(x => x.TrusteeId == trusteeId)
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync();

                if (admission == null)
                    throw new NotFoundException();

                DataContext.Admissions.Remove(admission);

                await DataContext.SaveChangesAsync();

                result = new SuccessResult();
            }
            catch (Exception e)
            {
                result = new ErrorResult(e);
            }

            return result;
        }

        [HttpGet("admissions/{id}/download")]
        public async Task<HttpResponseMessage> DownloadAdmissionPdf(Guid id)
        {
            HttpResponseMessage result = new HttpResponseMessage();

            try
            {
                var userId = GetCurrentUserId();

                var trusteeId =
                    await DataContext.Trustees.AsNoTracking()
                        .Where(x => x.OwnerID == userId)
                        .Select(x => x.Id)
                        .FirstOrDefaultAsync();

                var admission = await DataContext.Admissions
                    .Where(x => x.TrusteeId == trusteeId)
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync();

                await GeneratePdf(result);

            }
            catch (Exception e)
            {
                result = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                _logger.LogError(default(EventId), e, $"{nameof(DownloadAdmissionPdf)} failed");
            }

            return result;
        }

        private async Task GeneratePdf(HttpResponseMessage responseMessage)
        {
            //var path = Environment.CurrentDirectory + "/doc.pdf";
//            var os = new MemoryStream();

            var paragraph =
                "<html>" +
                "<body style=\"font-family: Arial;\">" +
                "<p style=\"width: 50%; float: right; text-align: right;\">" +
                "Директору государственного бюджетного " +
                "образовательного учреждения дополнительного " +
                "образования детей Центр технического творчества и " +
                "информационных технологий Ковалеву Д.С.<br/>" +
                "от ________________________________________<br/>" +
                "(Ф.И.О.законного представителя)<br/>" +
                "проживающего(щей) по адресу:<br/>" +
                "</p>" +
                "<p style=\"clear: both;\"></p>" +
                "<h2 style=\"text-align: center\">Заявление</h2>" +
                "</body>" +
                "</html>";

            var converter = new HtmlToPdf();

            converter.Options.PdfPageSize = PdfPageSize.A4;
            converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait;
            converter.Options.WebPageWidth = 800;
            converter.Options.WebPageHeight = 1024;

            converter.Options.MarginLeft = 25;
            converter.Options.MarginRight = 25;
            converter.Options.MarginTop = 30;
            converter.Options.MarginBottom = 30;

            var document = converter.ConvertHtmlString(paragraph);

            document.Save(Response.Body);

            Response.ContentType = "pdf/application";
            Response.Headers.Add("content-disposition",
            "attachment;filename=First PDF document.pdf");

            document.Close();
        }

        #endregion

    }
}
