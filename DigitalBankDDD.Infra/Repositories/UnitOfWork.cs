using DigitalBankDDD.Domain.Entities;
using DigitalBankDDD.Domain.Interfaces;
using DigitalBankDDD.Infra.Database;

namespace DigitalBankDDD.Infra.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly BankContext _context;
    private readonly Dictionary<string, object> _repositories = new();
    
    public UnitOfWork(BankContext context)
    {
        _context = context;
    }
    
    public IRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity
    {
        var type = typeof(TEntity).Name;

        if (_repositories.TryGetValue(type, out var repository)) 
            return (IRepository<TEntity>) repository;
        
        var repositoryInstance = new Repository<TEntity>(_context);
        _repositories.Add(type, repositoryInstance);

        return (IRepository<TEntity>)_repositories[type];
    }

    public async Task CommitAsync()
    {
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            await RollBackAsync();
            throw new Exception(ex.Message);
        }
    }
    
    public async Task RollBackAsync()
    {
        await _context.DisposeAsync(); 
    }
}