namespace DigitalBankDDD.Application;

public class ApplicationException : Exception
{
    public ApplicationException(string message) : base(message)
    {
    }
}