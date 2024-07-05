using AgendaApi.Application.Shared.Exceptions;
using AgendaApi.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AgendaApi.Application.UseCases.SuperAdminUseCases.GetSuperAdminByEmail
{
    public sealed class GetSuperAdminByEmailHandler
        : IRequestHandler<GetSuperAdminByEmailRequest, GetSuperAdminByEmailResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetSuperAdminByEmailHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetSuperAdminByEmailResponse> Handle(GetSuperAdminByEmailRequest request,
            CancellationToken cancellationToken)
        {
            var superAdmin = await _unitOfWork.SuperAdminRepository!.GetById(sa => sa.Email == request.email,
                cancellationToken);
            if (superAdmin is null) throw new NotFoundException("Usuário não encontrado.");

            return _mapper.Map<GetSuperAdminByEmailResponse>(superAdmin);
        }
    }
}
