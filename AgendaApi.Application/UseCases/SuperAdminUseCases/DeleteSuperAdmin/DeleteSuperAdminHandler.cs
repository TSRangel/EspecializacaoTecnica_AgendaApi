using AgendaApi.Application.Shared.Exceptions;
using AgendaApi.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AgendaApi.Application.UseCases.SuperAdminUseCases.DeleteSuperAdmin
{
    public sealed class DeleteSuperAdminHandler
        : IRequestHandler<DeleteSuperAdminRequest, DeleteSuperAdminResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DeleteSuperAdminHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<DeleteSuperAdminResponse> Handle(DeleteSuperAdminRequest request,
            CancellationToken cancellationToken)
        {
            var superAdmin = await _unitOfWork.SuperAdminRepository!.GetById(
                sa => sa.SuperAdminId == request.superAdminId, cancellationToken);
            if (superAdmin is null) throw new NotFoundException("Usuário não encotrado.");

            _unitOfWork.SuperAdminRepository.Delete(superAdmin);
            await _unitOfWork.Commit(cancellationToken);

            return _mapper.Map<DeleteSuperAdminResponse>(superAdmin);
        }
    }
}
