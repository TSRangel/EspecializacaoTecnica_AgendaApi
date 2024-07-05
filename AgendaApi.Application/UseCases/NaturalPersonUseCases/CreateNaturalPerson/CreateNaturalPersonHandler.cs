using AgendaApi.Application.Shared.Exceptions;
using AgendaApi.Application.Shared.GlobalValidators;
using AgendaApi.Application.Shared.PasswordHashing;
using AgendaApi.Domain.Entities;
using AgendaApi.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AgendaApi.Application.UseCases.NaturalPersonUseCases.CreateNaturalPerson
{
    public sealed class CreateNaturalPersonHandler :
        IRequestHandler<CreateNaturalPersonRequest, CreateNaturalPersonResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateNaturalPersonHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CreateNaturalPersonResponse> Handle(CreateNaturalPersonRequest request,
            CancellationToken cancellationToken)
        {
            if (await _unitOfWork.IsValidEmail(request.email, cancellationToken)) throw new BadRequestException("Email já cadastrado.");

            NaturalPerson naturalPerson = _mapper.Map<NaturalPerson>(request);
            naturalPerson.Password = PasswordHashingService.Hash(naturalPerson.Password!);
            _unitOfWork.NaturalPersonRepository!.Create(naturalPerson);
            await _unitOfWork.Commit(cancellationToken);

            return _mapper.Map<CreateNaturalPersonResponse>(naturalPerson);
        }
    }
}
