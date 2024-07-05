using AgendaApi.Application.Shared.Exceptions;
using AgendaApi.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AgendaApi.Application.UseCases.ServiceStatusUsecases.GetAllServiceStatus
{
    public sealed class GetAllServiceStatusHandler
        : IRequestHandler<GetAllServiceStatusRequest, List<GetAllServiceStatusResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetAllServiceStatusHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<GetAllServiceStatusResponse>> Handle(GetAllServiceStatusRequest request,
            CancellationToken cancellationToken)
        {
            var serviceStatus = await _unitOfWork.ServiceStatusRepository!.GetAll(cancellationToken);
            if (serviceStatus is null) throw new NotFoundException("Status de serviços não encontrados.");

            return _mapper.Map<List<GetAllServiceStatusResponse>>(serviceStatus);
        }
    }
}
