using AgendaApi.Domain.Entities;
using AgendaApi.Domain.Interfaces;
using AgendaApi.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AgendaApi.Persistence.Repositories
{
    public class TimetableRepository : BaseRepository<Timetable>, ITimetableRepository
    {
        public TimetableRepository(AgendaApiDbContext context) : base(context) { }
        public async Task<IEnumerable<Timetable>> Delete(CancellationToken cancellationToken)
        {
            var timetables = await Context.Timetables.ToListAsync(cancellationToken);
            Context.Timetables.RemoveRange(timetables);
            return timetables;
        }
        public async Task<IEnumerable<Timetable>> GetAllById(Expression<Func<Timetable, bool>> predicate, CancellationToken cancellationToken)
        {
            return await Context.Timetables.Where(predicate).ToListAsync(cancellationToken);
        }
    }
}
