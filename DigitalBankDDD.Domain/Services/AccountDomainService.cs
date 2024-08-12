using DigitalBankDDD.Domain.Entities;
using DigitalBankDDD.Domain.Exceptions;
using DigitalBankDDD.Domain.Interfaces;
using DigitalBankDDD.Domain.Utils;

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
        return CpfValidator.IsValid(cpf);
    }
    
    private static bool VerifyLegalAge(DateTime birthDate)
    {
        const int legalAge = 18;
        var today = DateTime.Today;
        var age = today.Year - birthDate.Year;
        return age >= legalAge;
    }
}