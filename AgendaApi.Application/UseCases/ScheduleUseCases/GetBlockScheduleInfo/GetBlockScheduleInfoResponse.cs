using AgendaApi.Application.UseCases.NaturalPersonUseCases.DTOs;
using AgendaApi.Application.UseCases.ServiceUseCase.DTOs;

namespace AgendaApi.Application.UseCases.ScheduleUseCases.GetBlockScheduleInfo
{
    public sealed record GetBlockScheduleInfoResponse
    {
        public GetServiceByName? blockService {  get; set; }
        public GetNaturalPersonByName? blockNaturalPerson {  get; set; }
    }

    public sealed record GetNaturalPersonByName : NaturalPersonBaseResponse;
    public sealed record GetServiceByName : ServiceBaseResponse;
}
