using AgendaApi.Domain.Entities;
using AgendaApi.Domain.Interfaces;
using AgendaApi.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AgendaApi.Persistence.Repositories
{
    public class LegalEntityRepository : BaseRepository<LegalEntity>, ILegalEntityRepository
    {
        public LegalEntityRepository(AgendaApiDbContext context) : base(context) { }
        public async Task<LegalEntity?> GetByEmail(Expression<Func<LegalEntity, bool>> predicate, CancellationToken cancellationToken)
        {
            return await Context.LegalEntities.AsNoTracking().FirstOrDefaultAsync(predicate, cancellationToken);
        }
    }
}
