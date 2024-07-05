using AgendaApi.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AgendaApi.Application.UseCases.LegalPersonUseCases.GetAllLegalEntity
{
    public sealed class GetAllLegalEntityHandler 
        : IRequestHandler<GetAllLegalEntityRequest, List<GetAllLegalEntityResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllLegalEntityHandler(IUnitOfWork unitOfWork, 
            IMapper mapper) 
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<GetAllLegalEntityResponse>> Handle(GetAllLegalEntityRequest request,
            CancellationToken cancellationToken)
        {
            var entities = await _unitOfWork.LegalEntityRepository!.GetAll(cancellationToken);
            return _mapper.Map<List<GetAllLegalEntityResponse>>(entities);
        }
    }
}
