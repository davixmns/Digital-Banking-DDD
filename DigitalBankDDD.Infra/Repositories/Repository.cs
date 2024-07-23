using System.Linq.Expressions;
using DigitalBankDDD.Domain.Entities;
using DigitalBankDDD.Domain.Interfaces;
using DigitalBankDDD.Infra.Database;
using Microsoft.EntityFrameworkCore;

namespace DigitalBankDDD.Infra.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
{
    protected readonly BankContext BankContext;
    
    public Repository(BankContext bankContext)
    {
        BankContext = bankContext;
    }
    
    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await BankContext.Set<TEntity>()
            .ToListAsync();
    }

    public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await BankContext.Set<TEntity>()
            .FirstOrDefaultAsync(predicate);
    }

    public TEntity Save(TEntity entity)
    {
        BankContext.Set<TEntity>().Add(entity);
        return entity;
    }

    public TEntity Update(TEntity entity)
    {
        BankContext.Set<TEntity>().Update(entity);
        return entity;
    }
    
    public TEntity Delete(TEntity entity)
    {
        BankContext.Set<TEntity>().Remove(entity);
        return entity;
    }
}