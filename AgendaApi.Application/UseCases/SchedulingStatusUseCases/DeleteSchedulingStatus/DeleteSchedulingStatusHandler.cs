using AgendaApi.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AgendaApi.Application.UseCases.SchedulingStatusUseCase.DeleteSchedulingStatus
{
    public sealed class DeleteSchedulingStatusHandler
        : IRequestHandler<DeleteSchedulingStatusRequest, DeleteSchedulingStatusResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DeleteSchedulingStatusHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<DeleteSchedulingStatusResponse> Handle(DeleteSchedulingStatusRequest request, 
            CancellationToken cancellationToken)
        {
            var schedulingStatus = await _unitOfWork.SchedulingStatusRepository!.GetById(ss => ss.SchedulingStatusId == request.id, cancellationToken);
            _unitOfWork.SchedulingStatusRepository.Delete(schedulingStatus!);
            await _unitOfWork.Commit(cancellationToken);

            return _mapper.Map<DeleteSchedulingStatusResponse>(schedulingStatus);
        }
    }
}
