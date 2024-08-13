using DigitalBankDDD.Domain.Entities;
using DigitalBankDDD.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace DigitalBankDDD.Infra.Context;

public class BankContext : DbContext
{
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    
    public BankContext(DbContextOptions<BankContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ConvertLongTextToVarchar(modelBuilder);
        
        ConvertValueObjects(modelBuilder);
        
        base.OnModelCreating(modelBuilder);
    }
    
    private void ConvertLongTextToVarchar(ModelBuilder modelBuilder)
    {
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var property in entity.GetProperties())
            {
                if (property.ClrType == typeof(string))
                {
                    // Define o tipo varchar com tamanho 255 por padrão
                    property.SetColumnType("varchar(255)"); 
                }
            }
        }
    }

    private void ConvertValueObjects(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Transaction>()
            .Property(t => t.Amount)
            .HasConversion(
                v => v.Value,
                v => new Amount(v)
            );
    }
}