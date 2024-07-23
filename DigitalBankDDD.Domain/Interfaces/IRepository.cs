using System.Linq.Expressions;
using DigitalBankDDD.Domain.Entities;

namespace DigitalBankDDD.Domain.Interfaces;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate);
    TEntity Save(TEntity entity);
    TEntity Update(TEntity entity);
    TEntity Delete(TEntity entity);
}