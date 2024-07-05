using AgendaApi.Application.Shared.Exceptions;
using AgendaApi.Application.UseCases.SuperAdminUseCases.UpdateSuperAdmin;
using AgendaApi.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AgendaApi.Application.UseCases.SuperAdminUseCases.UpdateSuperAdminn
{
    public sealed class UpdateSuperAdminHandler
        : IRequestHandler<UpdateSuperAdminRequest, UpdateSuperAdminResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateSuperAdminHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UpdateSuperAdminResponse> Handle(UpdateSuperAdminRequest request,
            CancellationToken cancellationToken)
        {
            var superAdmin = await _unitOfWork.SuperAdminRepository!.GetById(
                sa => sa.SuperAdminId == request.superAdminId, cancellationToken);
            if (superAdmin is null) throw new NotFoundException("Usuário não encontrado.");

            _mapper.Map(request, superAdmin);
            _unitOfWork.SuperAdminRepository.Update(superAdmin);
            await _unitOfWork.Commit(cancellationToken);

            return _mapper.Map<UpdateSuperAdminResponse>(superAdmin);
        }
    }
}
