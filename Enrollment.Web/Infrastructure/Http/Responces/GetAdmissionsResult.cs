using System.Collections;
using System.Collections.Generic;
using AutoMapper;
using Enrollment.Model;
using Enrollment.Web.Infrastructure.ViewModels;

namespace Enrollment.Web.Infrastructure.Http.Responces
{
    public class GetAdmissionsResult : SuccessResult
    {
        public AdmissionViewModel[] Admissions { get; set; }

        public GetAdmissionsResult(IEnumerable<Admission> admissions)
        {
            Admissions = Mapper.Map<AdmissionViewModel[]>(admissions);
        }
    }
}