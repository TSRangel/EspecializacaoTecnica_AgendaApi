using AgendaApi.Domain.Entities;
using AgendaApi.Domain.Interfaces;
using AgendaApi.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace AgendaApi.Persistence.Repositories
{
    public class WeekDayRepository : BaseRepository<WeekDay>, IWeekDayRepository
    {
        public WeekDayRepository(AgendaApiDbContext context) : base(context) { }
        public override async Task<WeekDay?> GetById(Expression<Func<WeekDay, bool>> predicate, CancellationToken cancellationToken)
        {
            return await Context.WeekDays.Include(wd => wd.Timetables).FirstOrDefaultAsync(predicate, cancellationToken);
        }
        public override async Task<IEnumerable<WeekDay>> GetAll(CancellationToken cancellationToken)
        {
            return await Context.WeekDays.Include(wd => wd.Timetables).ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<WeekDay>> GetAllById(Guid legalEntityId, CancellationToken cancellationToken)
        {
            return await Context.WeekDays.Include(wd => wd.Timetables!.Where(tt => tt.LegalEntityId == legalEntityId)).ToListAsync(cancellationToken);
        }
    }
}
