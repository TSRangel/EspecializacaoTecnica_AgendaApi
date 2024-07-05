using AgendaApi.Domain.Entities;
using AgendaApi.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AgendaApi.Application.UseCases.SchedulingStatusUseCase.CreateSchedulingStatus
{
    public sealed class CreateSchedulingStatusHandler
        : IRequestHandler<CreateSchedulingStatusRequest, CreateSchedulingStatusResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateSchedulingStatusHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<CreateSchedulingStatusResponse> Handle(CreateSchedulingStatusRequest request,
            CancellationToken cancellationToken)
        {
            var schedulingStatus = _mapper.Map<SchedulingStatus>(request);
            _unitOfWork.SchedulingStatusRepository!.Create(schedulingStatus);
            await _unitOfWork.Commit(cancellationToken);

            return _mapper.Map<CreateSchedulingStatusResponse>(schedulingStatus);
        }
    }
}
