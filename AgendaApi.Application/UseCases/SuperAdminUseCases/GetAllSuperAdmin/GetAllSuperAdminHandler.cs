using AgendaApi.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AgendaApi.Application.UseCases.SuperAdminUseCases.GetAllSuperAdmin
{
    public sealed class GetAllSuperAdminHandler
        : IRequestHandler<GetAllSuperAdminRequest, List<GetAllSuperAdminResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetAllSuperAdminHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<GetAllSuperAdminResponse>> Handle(GetAllSuperAdminRequest request,
            CancellationToken cancellationToken)
        {
            var superAdmins = await _unitOfWork.SuperAdminRepository!.GetAll(cancellationToken);
            return _mapper.Map<List<GetAllSuperAdminResponse>>(superAdmins);
        }
    }
}
