using AgendaApi.Application.Shared.Exceptions;
using AgendaApi.Application.Shared.GlobalValidators;
using AgendaApi.Application.Shared.PasswordHashing;
using AgendaApi.Domain.Entities;
using AgendaApi.Domain.Interfaces;
using AutoMapper;
using MediatR;
using System.Globalization;

namespace AgendaApi.Application.UseCases.LegalPersonUseCases.CreateLegalEntity
{
    public sealed class CreateLegalEntityHandler :
        IRequestHandler<CreateLegalEntityRequest, CreateLegalEntityResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateLegalEntityHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<CreateLegalEntityResponse> Handle(CreateLegalEntityRequest request,
            CancellationToken cancellationToken)
        {
            if (await _unitOfWork.IsValidEmail(request.email, cancellationToken)) throw new BadRequestException("Email já cadastrado.");

            var legalEntity = _mapper.Map<LegalEntity>(request);
            legalEntity.Password = PasswordHashingService.Hash(legalEntity.Password!);
            _unitOfWork.LegalEntityRepository!.Create(legalEntity);
            await _unitOfWork.Commit(cancellationToken);

            var searchedLegalEntity = await _unitOfWork.LegalEntityRepository.GetByEmail(le => le.Email == legalEntity.Email, cancellationToken);
            _unitOfWork.ServiceRepository!.Create(new Service
            {
                LegalEntityId = searchedLegalEntity!.LegalEntityId,
                Name = "Bloqueio",
                Description = "Serviço criado para ser utilizado como bloqueio de agenda.",
                Duration = TimeSpan.Parse("00:30:00", CultureInfo.InvariantCulture),
                Price = 0
            });
            await _unitOfWork.Commit(cancellationToken);

            return _mapper.Map<CreateLegalEntityResponse>(legalEntity);
        }
    }
}
