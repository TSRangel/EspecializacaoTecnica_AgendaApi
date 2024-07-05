using AgendaApi.Application.Shared.Exceptions;
using AgendaApi.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AgendaApi.Application.UseCases.SchedulingUseCases.GetAllNaturalPersonSchedulingsBySchedulingStatus
{
    public sealed class GetAllNaturalPersonSchedulingsBySchedulingStatusHandler
        : IRequestHandler<GetAllNaturalPersonSchedulingsBySchedulingStatusRequest,
            List<GetAllNaturalPersonSchedulingsBySchedulingStatusResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetAllNaturalPersonSchedulingsBySchedulingStatusHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<GetAllNaturalPersonSchedulingsBySchedulingStatusResponse>> Handle(GetAllNaturalPersonSchedulingsBySchedulingStatusRequest request,
            CancellationToken cancellationToken)
        {
            var schedulings = await _unitOfWork.SchedulingRepository!.GetAllByLegalEntityIdComplete(
                s => s.NaturalPersonId == request.naturalPersonId && s.SchedulingStatusId == request.schedulingStatusId, cancellationToken);
            if (schedulings is null) throw new NotFoundException("Não existe agendamentos com o estado o estado especificado para essa pessoa.");

            return _mapper.Map<List<GetAllNaturalPersonSchedulingsBySchedulingStatusResponse>>(schedulings);
        }
    }
}
