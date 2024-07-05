using AgendaApi.Application.UseCases.SchedulingUseCases.DTOs;
using AgendaApi.Application.UseCases.TimetableUseCases.GetAllTimetables;
using AgendaApi.Application.UseCases.WeekDayUseCases.GetAllWeekDay;

namespace AgendaApi.Application.UseCases.ScheduleUseCases.DTOs
{
    public abstract record BaseFreeMothScheduleResponse
    {
        public List<GetAllTimetablesResponse>? availableTimes { get; set; }
        public List<SchedulingInfo>? schedulingsInfo { get; set; }
    }

    public sealed record SchedulingInfo : SchedulingWithNaturalPersonInfo
    {

    }
}
