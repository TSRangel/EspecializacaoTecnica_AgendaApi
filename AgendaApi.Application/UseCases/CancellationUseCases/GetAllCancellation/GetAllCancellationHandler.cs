using AgendaApi.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AgendaApi.Application.UseCases.CancellationUseCase.GetAllCancellation
{
    public sealed class GetAllCancellationHandler
        : IRequestHandler<GetAllCancellationRequest, List<GetAllCancellationResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetAllCancellationHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<GetAllCancellationResponse>> Handle(GetAllCancellationRequest request,
            CancellationToken cancellationToken)
        {
            var cancellations = await _unitOfWork.CancellationRepository!.GetAll(cancellationToken);
            return _mapper.Map<List<GetAllCancellationResponse>>(cancellations);
        }
    }
}
