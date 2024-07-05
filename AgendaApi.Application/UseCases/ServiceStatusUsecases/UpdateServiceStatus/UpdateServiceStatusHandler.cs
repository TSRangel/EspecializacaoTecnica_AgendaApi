using AgendaApi.Application.Shared.Exceptions;
using AgendaApi.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AgendaApi.Application.UseCases.ServiceStatusUsecases.UpdateServiceStatus
{
    public sealed class UpdateServiceStatusHandler
        : IRequestHandler<UpdateServiceStatusRequest, UpdateServiceStatusResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateServiceStatusHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<UpdateServiceStatusResponse> Handle(UpdateServiceStatusRequest request,
            CancellationToken cancellationToken)
        {
            var serviceStatus = await _unitOfWork.ServiceStatusRepository!.GetById(ss => ss.ServiceStatusId == request.serviceStatusId, cancellationToken);
            if (serviceStatus is null) throw new NotFoundException("Status de serviço não encontrado.");

            _mapper.Map(request, serviceStatus);
            _unitOfWork.ServiceStatusRepository.Update(serviceStatus);
            await _unitOfWork.Commit(cancellationToken);
            return _mapper.Map<UpdateServiceStatusResponse>(serviceStatus);
        }
    }
}
