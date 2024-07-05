using AgendaApi.Domain.Entities;
using System.Linq.Expressions;

namespace AgendaApi.Domain.Interfaces
{
    public interface ILegalEntityRepository : IBaseRepository<LegalEntity>
    {
        Task<LegalEntity?> GetByEmail(Expression<Func<LegalEntity, bool>> predicate, CancellationToken cancellationToken);
    }
}
