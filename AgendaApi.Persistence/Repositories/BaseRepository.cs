using AgendaApi.Domain.Entities;
using AgendaApi.Domain.Interfaces;
using AgendaApi.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AgendaApi.Persistence.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity   
    {
        protected readonly AgendaApiDbContext Context;
        public BaseRepository(AgendaApiDbContext context)
        {
            Context = context;
        }
        public virtual void Create(T entity)
        {
            entity.CreatedAt = DateTime.Now;
            Context.Set<T>().Add(entity);
        }
        public virtual void Update(T entity)
        {
            entity.UpdatedAt = DateTime.Now;
            Context.Set<T>().Update(entity);
        }
        public void Delete(T entity)
        {
            entity.DeletedAt = DateTime.Now;
            Context.Set<T>().Remove(entity);
        }
        public async virtual Task<IEnumerable<T>> GetAll(CancellationToken cancellationToken)
        {
            return await Context.Set<T>().AsNoTracking().ToListAsync(cancellationToken);
        }
        public async virtual Task<T?> GetById(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
        {
            return await Context.Set<T>().AsNoTracking().FirstOrDefaultAsync(predicate, cancellationToken);
        }
    }
}
