using AgendaApi.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AgendaApi.Application.UseCases.ScheduleUseCases.GetBlockScheduleInfo
{
    public sealed class GetBlockScheduleInfoHandler
        : IRequestHandler<GetBlockScheduleInfoRequest, GetBlockScheduleInfoResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetBlockScheduleInfoHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<GetBlockScheduleInfoResponse> Handle(GetBlockScheduleInfoRequest request,
            CancellationToken cancellationToken)
        {
            var blockNaturalPerson = await _unitOfWork.NaturalPersonRepository!.GetById(np => np.Name == "Bloqueio", cancellationToken);
            var blockService = await _unitOfWork.ServiceRepository!.GetById(s => s.LegalEntityId == request.legalEntityId && s.Name == "Bloqueio", cancellationToken);
            return new GetBlockScheduleInfoResponse
            {
                blockService = _mapper.Map<GetServiceByName>(blockService),
                blockNaturalPerson = _mapper.Map<GetNaturalPersonByName>(blockNaturalPerson)
            };
        }
    }
}
