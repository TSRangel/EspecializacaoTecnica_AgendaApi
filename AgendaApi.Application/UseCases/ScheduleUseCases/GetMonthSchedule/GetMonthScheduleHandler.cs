using AgendaApi.Application.UseCases.ScheduleUseCases.DTOs;
using AgendaApi.Application.UseCases.TimetableUseCases.GetAllTimetables;
using AgendaApi.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AgendaApi.Application.UseCases.ScheduleUseCases.GetMonthSchedule
{
    public sealed class GetMonthScheduleHandler
        : IRequestHandler<GetMonthScheduleRequest, FreeMonthScheduleResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetMonthScheduleHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<FreeMonthScheduleResponse> Handle(GetMonthScheduleRequest request,
            CancellationToken cancellationToken)
        {
            var schedulings = await _unitOfWork.SchedulingRepository!.GetAllByDate(s => s.SchedulingDate.Month == request.date.Month && s.LegalEntityId == request.legalEntityId && s.SchedulingStatusId == 1, cancellationToken);
            var timetables = await _unitOfWork.TimetableRepository!.GetAllById(tt => tt.LegalEntityId == request.legalEntityId, cancellationToken);

            return new FreeMonthScheduleResponse
            {
                availableTimes = _mapper.Map<List<GetAllTimetablesResponse>>(timetables),
                schedulingsInfo = _mapper.Map<List<SchedulingInfo>>(schedulings)
            };
        }
    }
}
