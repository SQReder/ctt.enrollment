using AutoMapper;
using Enrollment.Model;
using Enrollment.Web.Infrastructure.ViewModels;

namespace Enrollment.Web.Infrastructure.Http.Responces
{
    public class GetAdmissionResult : SuccessResult
    {
        public AdmissionViewModel Admissions { get; set; }

        public GetAdmissionResult(Admission admission)
        {
            Admissions = Mapper.Map<AdmissionViewModel>(admission);
        }
    }
}