namespace AgendaApi.Application.UseCases.SchedulingStatusUseCase.DTOs
{
    public abstract record SchedulingStatusBaseResponse
    {
        public int SchedulingStatusId { get; set; }
        public string? StatusName { get; set; }
    }
}
