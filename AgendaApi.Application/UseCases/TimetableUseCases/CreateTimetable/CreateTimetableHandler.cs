using AgendaApi.Domain.Entities;
using AgendaApi.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AgendaApi.Application.UseCases.TimetableUseCases.CreateTimetable
{
    public class CreateTimetableHandler 
        : IRequestHandler<CreateMultipleTimetableRequest, List<CreateTimetableResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateTimetableHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<CreateTimetableResponse>> Handle(CreateMultipleTimetableRequest requests,
            CancellationToken cancellationToken)
        {
            foreach (var request in requests.TimetableRequests)
            {
                var timetable = _mapper.Map<Timetable>(request);
                _unitOfWork.TimetableRepository!.Create(timetable);
            }

            await _unitOfWork.Commit(cancellationToken);
            var timetables = await _unitOfWork.TimetableRepository!.GetAll(cancellationToken);
            return _mapper.Map<List<CreateTimetableResponse>>(timetables);
        }
    }
}
