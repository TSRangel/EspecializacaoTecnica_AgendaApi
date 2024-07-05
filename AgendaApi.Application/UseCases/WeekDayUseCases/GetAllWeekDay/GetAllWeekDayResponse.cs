using AgendaApi.Application.UseCases.SchedulingUseCases.DTOs;
using AgendaApi.Application.UseCases.TimetableUseCases.GetAllTimetables;
using AgendaApi.Application.UseCases.WeekDayUseCases.DTOs;

namespace AgendaApi.Application.UseCases.WeekDayUseCases.GetAllWeekDay
{
    public sealed record GetAllWeekDayResponse : WeekDayBaseResponse
    {
        public List<GetAllTimetablesResponse>? Timetables { get; set; }
    }
}
