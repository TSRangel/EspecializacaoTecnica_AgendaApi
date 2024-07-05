using AgendaApi.Application.Shared.Exceptions;
using AgendaApi.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AgendaApi.Application.UseCases.SchedulingUseCases.GetSchedulingById
{
    public sealed class GetSchedulingByIdHandler
        : IRequestHandler<GetSchedulingByIdRequest, GetSchedulingByIdResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetSchedulingByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<GetSchedulingByIdResponse> Handle(GetSchedulingByIdRequest request,
            CancellationToken cancellationToken)
        {
            var scheduling = await _unitOfWork.SchedulingRepository!.GetById(s => s.SchedulingId == request.id, cancellationToken);
            if (scheduling is null) throw new NotFoundException("Agendamento não encontrado.");
            return _mapper.Map<GetSchedulingByIdResponse>(scheduling);
        }
    }
}
