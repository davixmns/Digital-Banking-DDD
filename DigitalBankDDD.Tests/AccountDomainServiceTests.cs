using System.Reflection;
using System.Text.RegularExpressions;
using DigitalBankDDD.Domain.Services;

namespace DigitalBankDDD.Tests;

public class AccountDomainServiceTests
{
    private AccountDomainService _accountDomainService;

    [SetUp]
    public void Setup()
    {
        _accountDomainService = new AccountDomainService();
    }

    [Test]
    [TestCase("63417951305", true)] // Exemplo de CPF válido (substitua por um CPF válido real)
    [TestCase("12345678900", false)] // Exemplo de CPF inválido
    [TestCase("00000000000", false)] // CPF inválido por repetição
    [TestCase("", false)] // CPF inválido por ser vazio
    [TestCase("123", false)] // CPF inválido por comprimento insuficiente
    public void TestValidateCpf(string cpf, bool expectedResult)
    {
        var method = _accountDomainService.GetType()
            .GetMethod("ValidateCpf", BindingFlags.NonPublic | BindingFlags.Static);

        var result = (bool)method.Invoke(null, new object[] { cpf });

        Assert.AreEqual(expectedResult, result);
    }

    [Test]
    [TestCase("2010-07-26", false)] // Exemplo de data de nascimento inválida (menor de idade)
    [TestCase("2000-07-26", true)] // Exemplo de data de nascimento válida (maior de idade)
    [TestCase("1990-07-26", true)] // Exemplo de data de nascimento válida (maior de idade)
    [TestCase("2022-07-26", false)] // Exemplo de data de nascimento inválida (futura)
    public void TestVerifyLegalAge(string birthDateString, bool expectedResult)
    {
        var birthDate = DateOnly.Parse(birthDateString);

        var method = typeof(AccountDomainService)
            .GetMethod("VerifyLegalAge", BindingFlags.NonPublic | BindingFlags.Static);

        var result = (bool) method.Invoke(null, new object[] { birthDate });

        Assert.AreEqual(expectedResult, result);
    }
   
   [Test]
   [TestCase("+55 (85) 985974609", true)]
   [TestCase("(85) 98597-4609", true)]
   [TestCase("85 98597-4609", true)]
   [TestCase("85 985974609", true)]
   [TestCase("85985974609", true)]
   [TestCase("+5585985974609", true)]
   [TestCase("8598597460", false)] // Número incompleto
    public void PhoneNumberTest(string phoneNumber, bool expectedResult)
    {
        var result = Regex.IsMatch(phoneNumber, @"^(\+[0-9]{2,3}\s?)?\(?[0-9]{2}\)?\s?[0-9]{5}-?[0-9]{4}$");
        Assert.AreEqual(expectedResult, result);
    }
}