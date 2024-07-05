using AgendaApi.Application.Shared.PasswordHashing;
using AgendaApi.Domain.Entities;
using AgendaApi.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AgendaApi.Application.UseCases.SuperAdminUseCases.CreateSuperAdmin
{
    public sealed class CreateSuperAdminHandler
        : IRequestHandler<CreateSuperAdminRequest, CreateSuperAdminResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateSuperAdminHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CreateSuperAdminResponse> Handle(CreateSuperAdminRequest request,
            CancellationToken cancellationToken)
        {
            var superAdmin = _mapper.Map<SuperAdmin>(request);
            superAdmin.Password = PasswordHashingService.Hash(superAdmin.Password!);
            _unitOfWork.SuperAdminRepository!.Create(superAdmin);
            await _unitOfWork.Commit(cancellationToken);

            return _mapper.Map<CreateSuperAdminResponse>(superAdmin);
        }
    }
}
