using AgendaApi.Application.Shared.Exceptions;
using AgendaApi.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AgendaApi.Application.UseCases.ServiceUseCases.UpdateServiceAvailability
{
    public sealed class UpdateServiceAvailabilityHandler
        : IRequestHandler<UpdateServiceAvailabilityRequest, UpdateServiceAvailabilityResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateServiceAvailabilityHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<UpdateServiceAvailabilityResponse> Handle(UpdateServiceAvailabilityRequest request,
            CancellationToken cancellationToken)
        {
            var service = await _unitOfWork.ServiceRepository!.GetById(s => s.ServiceId == request.serviceId, cancellationToken);
            if (service is null) throw new NotFoundException("Serviço não encontrado.");

            service.ServiceStatusId = request.serviceStatusId;
            _unitOfWork.ServiceRepository.Update(service);
            await _unitOfWork.Commit(cancellationToken);
            return _mapper.Map<UpdateServiceAvailabilityResponse>(service);
        }
    }
}
