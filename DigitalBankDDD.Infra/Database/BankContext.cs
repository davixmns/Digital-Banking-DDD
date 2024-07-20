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
}