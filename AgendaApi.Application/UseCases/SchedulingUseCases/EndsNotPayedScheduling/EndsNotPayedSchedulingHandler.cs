using AgendaApi.Application.Shared.Exceptions;
using AgendaApi.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AgendaApi.Application.UseCases.SchedulingUseCases.EndsNotPayedScheduling
{
    public sealed class EndsNotPayedSchedulingHandler
        : IRequestHandler<EndsNotPayedSchedulingRequest, EndsNotPayedSchedulingResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public EndsNotPayedSchedulingHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<EndsNotPayedSchedulingResponse> Handle(EndsNotPayedSchedulingRequest request,
            CancellationToken cancellationToken)
        {
            var scheduling = await _unitOfWork.SchedulingRepository!.GetById(s => s.SchedulingId == request.schedulingId, cancellationToken);
            if (scheduling is null || scheduling.SchedulingStatusId != 1) throw new BadRequestException("Agendamentos cancelados não podem ser finalizados.");

            scheduling.SchedulingStatusId = 3;
            _unitOfWork.SchedulingRepository.Update(scheduling);
            await _unitOfWork.Commit(cancellationToken);

            return _mapper.Map<EndsNotPayedSchedulingResponse>(scheduling);
        }
    }
}
