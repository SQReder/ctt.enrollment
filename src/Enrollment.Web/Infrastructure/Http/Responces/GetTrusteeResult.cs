using AutoMapper;
using Enrollment.Model;
using Enrollment.Web.Infrastructure.ViewModels;

namespace Enrollment.Web.Infrastructure.Http.Responces
{
    public class GetTrusteeResult : SuccessResult
    {
        public TrusteeViewModel Trustee { get; set; }

        public GetTrusteeResult(Trustee trustee)
        {
            Trustee = Mapper.Map<TrusteeViewModel>(trustee);
        }
    }
}