using AgendaApi.Application.Shared.Exceptions;
using AgendaApi.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AgendaApi.Application.UseCases.ServiceUseCase.GetServiceById
{
    public sealed class GetServiceByIdHandler
        : IRequestHandler<GetServiceByIdRequest, GetServiceByIdResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetServiceByIdHandler(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetServiceByIdResponse> Handle(GetServiceByIdRequest request,
            CancellationToken cancellationToken)
        {
            var service = await _unitOfWork.ServiceRepository!.GetById(s => s.ServiceId == request.id,
                cancellationToken);
            if (service is null) throw new NotFoundException("Serviço não encontrado.");

            return _mapper.Map<GetServiceByIdResponse>(service);
        }
    }
}
