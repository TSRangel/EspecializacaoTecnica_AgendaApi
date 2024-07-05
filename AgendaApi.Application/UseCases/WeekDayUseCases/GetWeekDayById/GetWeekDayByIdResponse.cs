using AgendaApi.Application.UseCases.TimetableUseCases.GetAllTimetables;
using AgendaApi.Application.UseCases.WeekDayUseCases.DTOs;

namespace AgendaApi.Application.UseCases.WeekDayUseCases.GetWeekDayById
{
    public sealed record GetWeekDayByIdResponse : WeekDayBaseResponse
    {
        public ICollection<GetAllTimetablesResponse>? Timetables { get; set; }
    }
}
