using DigitalBankDDD.Domain.Entities;

namespace DigitalBankDDD.Domain.Interfaces;

public interface IUnitOfWork
{
    IRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity;
    Task CommitAsync();
}