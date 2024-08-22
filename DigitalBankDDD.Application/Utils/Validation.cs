using DigitalBankDDD.Domain.Entities;
using DigitalBankDDD.Domain.Interfaces;

namespace DigitalBankDDD.Application.Utils;

public static class Validation
{
    public static bool IsNull(object? obj)
    {
        return obj == null;
    }
    
    public static bool IsAnyNull(params object?[] objs)
    {
        return objs.Any(obj => obj == null);
    }
}