using AgendaApi.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AgendaApi.Application.UseCases.NaturalPersonUseCases.GetAllNaturalPerson
{
    public sealed class GetAllNaturalPersonHandler
        : IRequestHandler<GetAllNaturalPersonRequest, List<GetAllNaturalPersonResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllNaturalPersonHandler(IUnitOfWork unitOfWork, 
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<GetAllNaturalPersonResponse>> Handle(GetAllNaturalPersonRequest request,
            CancellationToken cancellationToken)
        {
            var naturalPersons = await _unitOfWork.NaturalPersonRepository!.GetAll(cancellationToken);
            return _mapper.Map<List<GetAllNaturalPersonResponse>>(naturalPersons.Where(np => np.Name != "Bloqueio"));
        }
    }
}
