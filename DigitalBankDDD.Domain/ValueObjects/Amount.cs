
namespace DigitalBankDDD.Domain.ValueObjects;

public class Amount
{
    public decimal Value { get; private set; }
    
    public Amount(decimal value)
    {
        if(value <= 0)
            throw new ApplicationException("Amount must be greater than 0");
        
        Value = value;
    }
}