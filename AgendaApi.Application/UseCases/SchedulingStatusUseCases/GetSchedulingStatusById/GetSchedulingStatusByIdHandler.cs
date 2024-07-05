using AgendaApi.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AgendaApi.Application.UseCases.SchedulingStatusUseCase.GetSchedulingStatusById
{
    public sealed class GetSchedulingStatusByIdHandler
        : IRequestHandler<GetSchedulingStatusByIdRequest, GetSchedulingStatusByIdResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetSchedulingStatusByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<GetSchedulingStatusByIdResponse> Handle(GetSchedulingStatusByIdRequest request, 
            CancellationToken cancellationToken)
        {
            var schedulingStatus = await _unitOfWork.SchedulingStatusRepository!.GetById(ss => ss.SchedulingStatusId == request.id, cancellationToken);
            return _mapper.Map<GetSchedulingStatusByIdResponse>(schedulingStatus);
        }
    }
}
