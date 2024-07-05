using AgendaApi.Domain.Entities;
using System.Linq.Expressions;

namespace AgendaApi.Domain.Interfaces
{
    public interface INaturalPersonRepository : IBaseRepository<NaturalPerson>
    {
        Task<NaturalPerson?> GetByEmail(Expression<Func<NaturalPerson, bool>> predicate, CancellationToken cancellationToken);
    }
}
