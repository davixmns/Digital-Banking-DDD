using DigitalBankDDD.Domain.Entities;
using DigitalBankDDD.Domain.Interfaces;
using FluentValidation;

namespace DigitalBankDDD.Application.Extensions;

internal static class ValidatorExtensions
{
    public static IRuleBuilderOptions<T, int> AccountExists<T>(this IRuleBuilder<T, int> ruleBuilder, IRepository<Account> userRepository)
    {
        return ruleBuilder.MustAsync(async (id, cancellationToken) =>
        {
            return await userRepository.GetAsync(a => a.Id == id) != null;
        }).WithMessage("Account does not existtttttt.");
    }
}