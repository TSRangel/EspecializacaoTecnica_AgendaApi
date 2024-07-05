using AgendaApi.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AgendaApi.Application.UseCases.TimetableUseCases.GetAllTimetables
{
    public sealed class GetAllTimetablesHandler
        : IRequestHandler<GetAllTimetablesRequest, List<GetAllTimetablesResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetAllTimetablesHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<GetAllTimetablesResponse>> Handle(GetAllTimetablesRequest request, 
            CancellationToken cancellationToken)
        {
            var timetables = await _unitOfWork.TimetableRepository!.GetAllById(tt => tt.LegalEntityId == request.legalEntityId,cancellationToken);
            return _mapper.Map<List<GetAllTimetablesResponse>>(timetables);
        }
    }
}
