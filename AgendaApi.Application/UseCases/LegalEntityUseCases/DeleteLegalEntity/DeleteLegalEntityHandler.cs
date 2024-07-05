using AgendaApi.Application.Shared.Exceptions;
using AgendaApi.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AgendaApi.Application.UseCases.LegalPersonUseCases.DeleteLegalEntity
{
    public sealed class DeleteLegalEntityHandler : 
        IRequestHandler<DeleteLegalEntityRequest, DeleteLegalEntityResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteLegalEntityHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<DeleteLegalEntityResponse> Handle(DeleteLegalEntityRequest request,
            CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.LegalEntityRepository!.GetById(
                le => le.LegalEntityId == request.Id, cancellationToken);
            if (entity is null) throw new NotFoundException("Usuário não encontrado.");

            _unitOfWork.LegalEntityRepository.Delete(entity);
            await _unitOfWork.Commit(cancellationToken);

            return _mapper.Map<DeleteLegalEntityResponse>(entity);
        }
    }
}
