using AgendaApi.Application.Shared.Exceptions;
using AgendaApi.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AgendaApi.Application.UseCases.NaturalPersonUseCases.DeleteNaturalPerson
{
    public sealed class DeleteNaturalPersonHandler 
        : IRequestHandler<DeleteNaturalPersonRequest, DeleteNaturalPersonResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteNaturalPersonHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<DeleteNaturalPersonResponse> Handle(DeleteNaturalPersonRequest request,
            CancellationToken cancellationToken)
        {
            var naturalPerson = await _unitOfWork.NaturalPersonRepository!.GetById(
                np => np.NaturalPersonId ==request.id, cancellationToken);
            
            if (naturalPerson is null) throw new NotFoundException("Usuário não encontrado.");

            _unitOfWork.NaturalPersonRepository.Delete(naturalPerson);
            await _unitOfWork.Commit(cancellationToken);

            return _mapper.Map<DeleteNaturalPersonResponse>(naturalPerson);
        }
    }
}
