using DigitalBankDDD.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DigitalBankDDD.Infra.Database;

public class BankContext : DbContext
{
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    
    public BankContext(DbContextOptions<BankContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var property in entity.GetProperties())
            {
                if (property.ClrType == typeof(string))
                {
                    property.SetColumnType("varchar(255)"); // Define o tipo varchar com tamanho 255 por padrão
                }
            }
        }
        base.OnModelCreating(modelBuilder);
    }
}