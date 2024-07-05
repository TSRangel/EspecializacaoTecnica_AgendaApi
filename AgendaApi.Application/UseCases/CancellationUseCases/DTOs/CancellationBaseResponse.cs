using AgendaApi.Domain.Entities;

namespace AgendaApi.Application.UseCases.CancellationUseCase.DTOs
{
    public abstract record CancellationBaseResponse
    {
        public Guid CancellationId { get; set; }
        public DateTime? CancellationTime { get; set; }
        public Guid Owner { get; set; }
        public Guid? SchedulingId { get; set; }
        public Scheduling? Scheduling { get; set; }
    }
}
