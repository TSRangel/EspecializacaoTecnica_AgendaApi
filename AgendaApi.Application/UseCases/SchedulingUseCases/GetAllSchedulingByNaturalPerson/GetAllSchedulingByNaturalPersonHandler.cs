using AgendaApi.Application.Shared.Exceptions;
using AgendaApi.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AgendaApi.Application.UseCases.SchedulingUseCases.GetAllSchedulingByNaturalPerson
{
    public sealed class GetAllSchedulingByNaturalPersonHandler
        : IRequestHandler<GetAllSchedulingByNaturalPersonRequest, List<GetAllSchedulingByNaturalPersonResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetAllSchedulingByNaturalPersonHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<GetAllSchedulingByNaturalPersonResponse>> Handle(GetAllSchedulingByNaturalPersonRequest request,
            CancellationToken cancellationToken)
        {
            var schedulings = await _unitOfWork.SchedulingRepository!.GetAllByNaturalPersonIdComplete(s => s.NaturalPersonId == request.naturalPersonId && s.SchedulingStatusId == 1, cancellationToken);
            if (schedulings is null) throw new NotFoundException("Não existem agendamentos para a pessoa selecionada.");

            return _mapper.Map<List<GetAllSchedulingByNaturalPersonResponse>>(schedulings);
        }
    }
}
