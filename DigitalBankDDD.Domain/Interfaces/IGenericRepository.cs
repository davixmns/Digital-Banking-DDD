using System.Linq.Expressions;

namespace DigitalBankDDD.Domain.Interfaces;

public interface IGenericRepository<TEntity> where TEntity : class
{
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate);
    TEntity Save(TEntity entity);
    TEntity Update(TEntity entity);
    TEntity Delete(TEntity entity);
}