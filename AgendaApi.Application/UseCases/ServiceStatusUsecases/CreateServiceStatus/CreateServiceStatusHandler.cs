using AgendaApi.Domain.Entities;
using AgendaApi.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AgendaApi.Application.UseCases.ServiceStatusUsecases.CreateServiceStatus
{
    public sealed class CreateServiceStatusHandler
        : IRequestHandler<CreateServiceStatusRequest, CreateServiceStatusResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateServiceStatusHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<CreateServiceStatusResponse> Handle(CreateServiceStatusRequest request, 
            CancellationToken cancellationToken)
        {
            var serviceStatus = _mapper.Map<ServiceStatus>(request);
            _unitOfWork.ServiceStatusRepository!.Create(serviceStatus);
            await _unitOfWork.Commit(cancellationToken);
            return _mapper.Map<CreateServiceStatusResponse>(serviceStatus);
        }
    }
}
