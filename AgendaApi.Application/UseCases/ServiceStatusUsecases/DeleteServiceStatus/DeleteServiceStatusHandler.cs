using AgendaApi.Application.Shared.Exceptions;
using AgendaApi.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AgendaApi.Application.UseCases.ServiceStatusUsecases.DeleteServiceStatus
{
    public sealed class DeleteServiceStatusHandler
        : IRequestHandler<DeleteServiceStatusRequest, DeleteServiceStatusResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DeleteServiceStatusHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<DeleteServiceStatusResponse> Handle(DeleteServiceStatusRequest request,
            CancellationToken cancellationToken)
        {
            var serviceStatus = await _unitOfWork.ServiceStatusRepository!.GetById(ss => ss.ServiceStatusId == request.serviceStatusId, cancellationToken);
            if (serviceStatus == null) throw new NotFoundException("Status de serviço não encontrado.");

            _unitOfWork.ServiceStatusRepository.Delete(serviceStatus);
            await _unitOfWork.Commit(cancellationToken);
            return _mapper.Map<DeleteServiceStatusResponse>(serviceStatus);
        }
    }
}
