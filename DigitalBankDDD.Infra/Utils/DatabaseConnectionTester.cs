using DigitalBankDDD.Infra.Database;
using Microsoft.EntityFrameworkCore;

namespace DigitalBankDDD.Infra.Utils;

public class DatabaseConnectionTester
{
    public static void TestConnection(BankContext context)
    {
        context.Database.OpenConnection();
        context.Database.CloseConnection();

        Console.WriteLine("Database connected!");
    }
}