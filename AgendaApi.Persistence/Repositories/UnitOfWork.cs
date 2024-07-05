using AgendaApi.Domain.Interfaces;
using AgendaApi.Persistence.Context;

namespace AgendaApi.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ISuperAdminRepository _superAdminRepository;
        private readonly ILegalEntityRepository _legalEntityRepository;
        private readonly INaturalPersonRepository _naturalPersonRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IServiceStatusRepository _serviceStatusRepository;
        private readonly IWeekDayRepository _weekDayRepository;
        private readonly ITimetableRepository _timetableRepository;
        private readonly ISchedulingStatusRepository _schedulingStatusRepository;
        private readonly ISchedulingRepository _schedulingRepository;
        private readonly ICancellationRepository _cancellationRepository;
        private readonly AgendaApiDbContext _context;
        public UnitOfWork(AgendaApiDbContext context)
        {
            _context = context;
        }
        public ISuperAdminRepository SuperAdminRepository => 
            _superAdminRepository ?? new SuperAdminRepository(_context);
        public ILegalEntityRepository LegalEntityRepository =>
            _legalEntityRepository ?? new LegalEntityRepository(_context);
        public INaturalPersonRepository NaturalPersonRepository =>
            _naturalPersonRepository ?? new NaturalPersonRepository(_context);
        public IServiceRepository ServiceRepository =>
            _serviceRepository ?? new ServiceRepository(_context);
        public IServiceStatusRepository ServiceStatusRepository =>
            _serviceStatusRepository ?? new ServiceStatusRepository(_context);
        public IWeekDayRepository WeekDayRepository =>
            _weekDayRepository ?? new WeekDayRepository(_context);
        public ITimetableRepository TimetableRepository =>
            _timetableRepository ?? new TimetableRepository(_context);
        public ISchedulingStatusRepository SchedulingStatusRepository =>
            _schedulingStatusRepository ?? new SchedulingStatusRepository(_context);
        public ISchedulingRepository SchedulingRepository =>
            _schedulingRepository ?? new SchedulingRepository(_context);
        public ICancellationRepository CancellationRepository =>
            _cancellationRepository ?? new CancellationRepository(_context);
        public async Task Commit(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
