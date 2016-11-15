using System.Collections;
using AutoMapper;
using Enrollment.Web.Infrastructure.ViewModels;

namespace Enrollment.Web.Infrastructure.Http.Responces
{
    public class GetEnrolleesResult : SuccessResult
    {
        public EnrolleeViewModel[] Enrollees { get; set; }

        public GetEnrolleesResult(IEnumerable enrollees)
        {
            Enrollees = Mapper.Map<EnrolleeViewModel[]>(enrollees);
        }
    }
}