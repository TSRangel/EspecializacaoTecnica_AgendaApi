using AgendaApi.Application.Shared.Exceptions;
using AgendaApi.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AgendaApi.Application.UseCases.CancellationUseCase.GetAllCancellationByOwner
{
    public class GetAllCancellationByOwnerHandler 
        : IRequestHandler<GetAllCancellationByOwnerRequest, List<GetAllCancellationByOwnerResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetAllCancellationByOwnerHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<GetAllCancellationByOwnerResponse>> Handle(GetAllCancellationByOwnerRequest request,
            CancellationToken cancellationToken)
        {
            var cancellations = await _unitOfWork.CancellationRepository!.GetAllByOwner(c => c.Owner == request.id, cancellationToken);
            if (cancellations is null) throw new NotFoundException("Não existem cancelamentos realizados pelo usuário.");
            return _mapper.Map<List<GetAllCancellationByOwnerResponse>>(cancellations);
        }
    }
}
