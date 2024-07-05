using AgendaApi.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AgendaApi.Application.UseCases.SchedulingUseCases.GetAllScheduling
{
    public sealed class GetAllSchedulingHandle
        : IRequestHandler<GetAllSchedulingRequest, List<GetAllSchedulingResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetAllSchedulingHandle(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<GetAllSchedulingResponse>> Handle(GetAllSchedulingRequest request,
            CancellationToken cancellationToken)
        {
            var schedulings = await _unitOfWork.SchedulingRepository!.GetAll(cancellationToken);
            return _mapper.Map<List<GetAllSchedulingResponse>>(schedulings);
        }
    }
}
