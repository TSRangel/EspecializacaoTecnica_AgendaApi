using AgendaApi.Domain.Entities;
using AgendaApi.Domain.Interfaces;
using AgendaApi.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AgendaApi.Persistence.Repositories
{
    public class SuperAdminRepository : BaseRepository<SuperAdmin>, ISuperAdminRepository
    {
        public SuperAdminRepository(AgendaApiDbContext context) : base(context) { }
        public async Task<SuperAdmin?> GetByEmail(Expression<Func<SuperAdmin, bool>> predicate, CancellationToken cancellationToken)
        {
            return await Context.SuperAdmins.AsNoTracking().FirstOrDefaultAsync(predicate, cancellationToken);
        }
    }
}
