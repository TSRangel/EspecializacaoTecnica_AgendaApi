namespace AgendaApi.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        ILegalEntityRepository? LegalEntityRepository { get; }
        INaturalPersonRepository? NaturalPersonRepository { get; }
        IServiceRepository? ServiceRepository { get; }
        IServiceStatusRepository? ServiceStatusRepository { get; }
        IWeekDayRepository? WeekDayRepository { get; }
        ITimetableRepository? TimetableRepository { get; }
        ISchedulingRepository? SchedulingRepository { get; }
        ICancellationRepository? CancellationRepository { get; }
        ISchedulingStatusRepository? SchedulingStatusRepository { get; }
        ISuperAdminRepository? SuperAdminRepository { get; }
        Task Commit(CancellationToken cancellationToken);
    }
}
