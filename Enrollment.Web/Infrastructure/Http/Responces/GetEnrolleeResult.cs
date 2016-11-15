using AutoMapper;
using Enrollment.Model;
using Enrollment.Web.Infrastructure.ViewModels;

namespace Enrollment.Web.Infrastructure.Http.Responces
{
    public class GetEnrolleeResult : GenericResult
    {
        public EnrolleeViewModel Enrollee { get; set; }

        public GetEnrolleeResult(Enrollee enrollee) : base(true)
        {
            Enrollee = Mapper.Map<EnrolleeViewModel>(enrollee);
        }
    }
}