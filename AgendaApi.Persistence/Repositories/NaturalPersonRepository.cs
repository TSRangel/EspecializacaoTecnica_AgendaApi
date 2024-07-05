using AgendaApi.Domain.Entities;
using AgendaApi.Domain.Interfaces;
using AgendaApi.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AgendaApi.Persistence.Repositories
{
    public class NaturalPersonRepository : BaseRepository<NaturalPerson>, INaturalPersonRepository
    {
        public NaturalPersonRepository(AgendaApiDbContext context) : base(context) { }
        public async Task<NaturalPerson?> GetByEmail(Expression<Func<NaturalPerson, bool>> predicate, CancellationToken cancellationToken)
        {
            return await Context.NaturalPersons.AsNoTracking().FirstOrDefaultAsync(predicate, cancellationToken);
        }
    }
}
