using AutoMapper;
using Enrollment.Api.Infrastructure.ViewModels;
using Enrollment.Model;

namespace Enrollment.Api.Infrastructure
{
    public static class MappingConfig
    {
        public static void Configure(IMapperConfigurationExpression configuration)
        {

            configuration.CreateMap<Enrollee, EnrolleeViewModel>()
                .ForMember(
                    dest => dest.Address,
                    opts => opts.MapFrom(src => src.Address.Raw));
            configuration.CreateMap<EnrolleeViewModel, Enrollee>()
                .ForMember(
                    dest => dest.Address,
                    opts => opts.MapFrom(src => new Address {Raw = src.Address}));

            configuration.CreateMap<UnityGroup, UnityGroupViewModel>();
            configuration.CreateMap<UnityGroupViewModel, UnityGroup>();

            configuration.CreateMap<Unity, UnityViewModel>();
            configuration.CreateMap<UnityViewModel, Unity>();

            configuration.CreateMap<Address, AddressViewModel>();

            configuration.CreateMap<Trustee, TrusteeViewModel>()
                .ForMember(
                    dest => dest.Address,
                    opts => opts.MapFrom(src => src.Address.Raw));
            configuration.CreateMap<TrusteeViewModel, Trustee>()
                .ForMember(
                    dest => dest.Address,
                    opts => opts.MapFrom(src => new Address {Raw = src.Address}));

            configuration.CreateMap<Admission, AdmissionViewModel>();
            configuration.CreateMap<AdmissionViewModel, Admission>();
        }
    }
}