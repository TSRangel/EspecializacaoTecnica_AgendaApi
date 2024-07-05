using AgendaApi.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AgendaApi.Application.UseCases.SchedulingStatusUseCase.GetAllSchedulingStatus
{
    public sealed class GetAllSchedulingStatusHandler 
        : IRequestHandler<GetAllSchedulingStatusRequest, List<GetAllSchedulingStatusResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetAllSchedulingStatusHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<GetAllSchedulingStatusResponse>> Handle(GetAllSchedulingStatusRequest request, 
            CancellationToken cancellationToken)
        {
            var schedulingStatus = await _unitOfWork.SchedulingStatusRepository!.GetAll(cancellationToken);
            return _mapper.Map<List<GetAllSchedulingStatusResponse>>(schedulingStatus);
        }
    }
}
