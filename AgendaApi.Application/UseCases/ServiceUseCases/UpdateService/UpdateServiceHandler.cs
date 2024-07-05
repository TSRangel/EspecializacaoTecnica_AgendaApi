using AgendaApi.Application.Shared.Exceptions;
using AgendaApi.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AgendaApi.Application.UseCases.ServiceUseCase.UpdateService
{
    public sealed class UpdateServiceHandler
        : IRequestHandler<UpdateServiceRequest, UpdateServiceResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateServiceHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<UpdateServiceResponse> Handle(UpdateServiceRequest request,
            CancellationToken cancellationToken)
        {
            var service = await _unitOfWork.ServiceRepository!.GetById(s => s.ServiceId == request.serviceId,
                cancellationToken);

            if (service is null) throw new NotFoundException("Serviço não encontrado.");

            _mapper.Map(request, service);
            _unitOfWork.ServiceRepository.Update(service);
            await _unitOfWork.Commit(cancellationToken);

            return _mapper.Map<UpdateServiceResponse>(service);
        }
    }
}