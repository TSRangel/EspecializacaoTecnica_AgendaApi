using AgendaApi.Domain.Entities;
using AutoMapper;

namespace AgendaApi.Application.UseCases.SchedulingUseCases.GetAllSchedulingByLegalEntity
{
    public sealed class GetAllSchedulingByLegalEntityMapper : Profile
    {
        public GetAllSchedulingByLegalEntityMapper() 
        {
            CreateMap<Scheduling, GetAllSchedulingByLegalEntityResponse>()
                .ForMember(dest => dest.schedulingId, option => option.MapFrom(src => src.SchedulingId))
                .ForMember(dest => dest.schedulingDate, option => option.MapFrom(src => src.SchedulingDate))
                .ForMember(dest => dest.legalEntityId, option => option.MapFrom(src => src.LegalEntityId))
                .ForMember(dest => dest.schedulingStatusName, option => option.MapFrom(src => src.SchedulingStatus!.StatusName))
                .ForMember(dest => dest.serviceName, option => option.MapFrom(src => src.Service!.Name))
                .ForMember(dest => dest.serviceDuration, option => option.MapFrom(src => src.Service!.Duration))
                .ForMember(dest => dest.naturalPersonName, option => option.MapFrom(src => src.NaturalPerson!.Name))
                .ForMember(dest => dest.naturalPersonPhone, option => option.MapFrom(src => src.NaturalPerson!.PhoneNumber));
        }
    }
}
