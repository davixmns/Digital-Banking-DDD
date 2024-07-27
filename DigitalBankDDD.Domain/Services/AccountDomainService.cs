using DigitalBankDDD.Domain.Entities;
using DigitalBankDDD.Domain.Exceptions;
using DigitalBankDDD.Domain.Interfaces;

namespace DigitalBankDDD.Domain.Services;

public sealed class AccountDomainService : IAccountDomainService
{
    public void ValidateAccountCreation(Account account)
    {
        if(!ValidateCpf(account.Cpf))
            throw new DomainException("Invalid CPF.");
        
        if(!VerifyLegalAge(account.BirthDate))
            throw new DomainException("The account holder must be of legal age.");
    }

    private static bool ValidateCpf(string cpf)
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
    
    private static bool VerifyLegalAge(DateOnly birthDate)
    {
        const int legalAge = 18;
        var today = DateOnly.FromDateTime(DateTime.Now);
        var age = today.Year - birthDate.Year;
        return age >= legalAge;
    }
}