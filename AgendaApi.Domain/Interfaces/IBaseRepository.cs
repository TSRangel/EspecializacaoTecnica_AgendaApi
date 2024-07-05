using AgendaApi.Domain.Entities;
using System.Linq.Expressions;

namespace AgendaApi.Domain.Interfaces
{
    public interface IBaseRepository<T>
    {
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<IEnumerable<T>> GetAll(CancellationToken cancellationToken);
        Task<T?> GetById(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
    }
}
