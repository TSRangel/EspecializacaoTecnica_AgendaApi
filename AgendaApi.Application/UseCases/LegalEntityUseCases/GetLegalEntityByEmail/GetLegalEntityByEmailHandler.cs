using AgendaApi.Application.Shared.Exceptions;
using AgendaApi.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AgendaApi.Application.UseCases.LegalPersonUseCases.GetLegalEntityByEmail
{
    public sealed class GetLegalEntityByEmailHandler 
        : IRequestHandler<GetLegalEntityByEmailRequest, GetLegalEntityByEmailResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetLegalEntityByEmailHandler(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetLegalEntityByEmailResponse> Handle(GetLegalEntityByEmailRequest request,
            CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.LegalEntityRepository!.GetByEmail(le => le.Email == request.email, cancellationToken);
            if (entity is null) throw new NotFoundException("Usuário não encontrado.");

            return _mapper.Map<GetLegalEntityByEmailResponse>(entity);
        }
    }
}
