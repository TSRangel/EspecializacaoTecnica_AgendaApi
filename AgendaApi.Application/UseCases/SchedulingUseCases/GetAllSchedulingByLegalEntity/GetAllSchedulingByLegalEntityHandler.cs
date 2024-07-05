using AgendaApi.Application.Shared.Exceptions;
using AgendaApi.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AgendaApi.Application.UseCases.SchedulingUseCases.GetAllSchedulingByLegalEntity
{
    public sealed class GetAllSchedulingByLegalEntityHandler
        : IRequestHandler<GetAllSchedulingByLegalEntityRequest, List<GetAllSchedulingByLegalEntityResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetAllSchedulingByLegalEntityHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<GetAllSchedulingByLegalEntityResponse>> Handle(GetAllSchedulingByLegalEntityRequest request,
            CancellationToken cancellationToken)
        {
            var schedulings = await _unitOfWork.SchedulingRepository!.GetAllByLegalEntityIdComplete(
                s => s.LegalEntityId == request.legalEntityId, cancellationToken);
            if (schedulings is null) throw new NotFoundException("Não existem agendamentos para a pessoa selecionada.");

            return _mapper.Map<List<GetAllSchedulingByLegalEntityResponse>>(schedulings);
        }
    }
}