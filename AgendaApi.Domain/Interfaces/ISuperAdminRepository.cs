using AgendaApi.Domain.Entities;
using System.Linq.Expressions;

namespace AgendaApi.Domain.Interfaces
{
    public interface ISuperAdminRepository : IBaseRepository<SuperAdmin>
    {
        Task<SuperAdmin?> GetByEmail(Expression<Func<SuperAdmin, bool>> predicate, CancellationToken cancellationToken);
    }
}
