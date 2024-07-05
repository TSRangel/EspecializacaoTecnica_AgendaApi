using AgendaApi.Domain.Entities;
using AutoMapper;

namespace AgendaApi.Application.UseCases.SchedulingUseCases.GetAllSchedulingByNaturalPerson
{
    public sealed class GetAllSchedulingByNaturalPersonMapper : Profile
    {
        public GetAllSchedulingByNaturalPersonMapper() 
        {
            CreateMap<Scheduling, GetAllSchedulingByNaturalPersonResponse>()
                .ForMember(dest => dest.schedulingId, option => option.MapFrom(src => src.SchedulingId))
                .ForMember(dest => dest.schedulingDate, option => option.MapFrom(src => src.SchedulingDate))
                .ForMember(dest => dest.legalEntityId, option => option.MapFrom(src => src.LegalEntityId))
                .ForMember(dest => dest.schedulingStatusName, option => option.MapFrom(src => src.SchedulingStatus!.StatusName))
                .ForMember(dest => dest.serviceName, option => option.MapFrom(src => src.Service!.Name))
                .ForMember(dest => dest.serviceDuration, option => option.MapFrom(src => src.Service!.Duration))
                .ForMember(dest => dest.legalEntityName, option => option.MapFrom(src => src.LegalEntity!.Name))
                .ForMember(dest => dest.legalEntityPhone, option => option.MapFrom(src => src.LegalEntity!.PhoneNumber));
        }
    }
}
