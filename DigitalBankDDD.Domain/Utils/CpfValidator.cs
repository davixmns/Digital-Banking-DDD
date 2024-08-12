namespace DigitalBankDDD.Domain.Utils;

public class CpfValidator
{
    public static bool IsValid(string cpf)
    {
        var invalidsCpf = new[]
        {
            "00000000000", "11111111111", "22222222222", "33333333333",
            "44444444444", "55555555555", "66666666666", "77777777777",
            "88888888888", "99999999999"
        };
        
        if (string.IsNullOrWhiteSpace(cpf) || cpf.Length != 11 || invalidsCpf.Contains(cpf))
            return false;
        
        var cpfArray = cpf.Select(c => int.Parse(c.ToString())).ToArray();
        
        var firstDigitSum = 0;
        
        for (var i = 0; i < 9; i++)
            firstDigitSum += cpfArray[i] * (10 - i);
        
        var calculatedFirstDigit = (firstDigitSum * 10) % 11;
        
        if (calculatedFirstDigit is 10 or 11)
            calculatedFirstDigit = 0;
        
        if(!calculatedFirstDigit.Equals(cpfArray[9]))
            return false;
        
        var secondDigitSum = 0;
        
        for (var i = 0; i < 10; i++)
            secondDigitSum += cpfArray[i] * (11 - i);
        
        var calculatedSecondDigit = (secondDigitSum * 10) % 11;
        
        if (calculatedSecondDigit is 10 or 11)
            calculatedSecondDigit = 0;
        
        if(!calculatedSecondDigit.Equals(cpfArray[10]))
            return false;
        
        return true;
    }
}