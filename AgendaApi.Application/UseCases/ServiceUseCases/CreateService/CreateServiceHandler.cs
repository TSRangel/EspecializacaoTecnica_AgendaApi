using AgendaApi.Domain.Entities;
using AgendaApi.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AgendaApi.Application.UseCases.ServiceUseCase.CreateService
{
    public sealed class CreateServiceHandler
        : IRequestHandler<CreateServiceRequest, CreateServiceResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreateServiceHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<CreateServiceResponse> Handle(CreateServiceRequest request,
            CancellationToken cancellationToken)
        {
            var service = _mapper.Map<Service>(request);
            _unitOfWork.ServiceRepository!.Create(service);
            await _unitOfWork.Commit(cancellationToken);

            return _mapper.Map<CreateServiceResponse>(service);
        }
    }
}
