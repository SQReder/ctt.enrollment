using AutoMapper;
using Enrollment.Model;
using Enrollment.Web.Infrastructure.ViewModels;

namespace Enrollment.Web.Infrastructure.Http.Responces
{
    public class GetTrusteeAddressResult : SuccessResult
    {
        public AddressViewModel Address { get; set; }

        public GetTrusteeAddressResult(Address address)
        {
            Address = Mapper.Map<AddressViewModel>(address);
        }
    }
}