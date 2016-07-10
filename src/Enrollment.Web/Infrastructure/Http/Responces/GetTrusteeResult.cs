using Enrollment.Web.Infrastructure.ViewModels;

namespace Enrollment.Web.Infrastructure.Http.Responces
{
    public class GetTrusteeResult : SuccessResult
    {
        public TrusteeViewModel Trustee { get; set; }

        public GetTrusteeResult(TrusteeViewModel trustee)
        {
            Trustee = trustee;
        }
    }
}