using AgendaApi.Application.Shared.Exceptions;
using AgendaApi.Domain.Entities;
using AgendaApi.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AgendaApi.Application.UseCases.SchedulingUseCases.DeleteScheduling
{
    public sealed class CancelSchedulingHandler
        : IRequestHandler<CancelSchedulingRequest, CancelSchedulingResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CancelSchedulingHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<CancelSchedulingResponse> Handle(CancelSchedulingRequest request,
            CancellationToken cancellationToken)
        {
            var scheduling = await _unitOfWork.SchedulingRepository!.GetById(s => s.SchedulingId == request.schedulingId, cancellationToken);
            if (scheduling is null || scheduling.SchedulingStatusId != 1) throw new BadRequestException("Somente agendamentos não finalizados podem ser cancelados.");
            
            scheduling.SchedulingStatusId = 4;
            _unitOfWork.SchedulingRepository.Update(scheduling);

            var cancellation = _mapper.Map<Cancellation>(request);
            cancellation.CancellationTime = DateTime.Now;
            _unitOfWork.CancellationRepository!.Create(cancellation);
            await _unitOfWork.Commit(cancellationToken);

            return _mapper.Map<CancelSchedulingResponse>(scheduling);
        }
    }
}
