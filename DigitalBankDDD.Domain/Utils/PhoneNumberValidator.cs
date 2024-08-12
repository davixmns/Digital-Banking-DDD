using System.Text.RegularExpressions;

namespace DigitalBankDDD.Domain.Utils;

public class PhoneNumberValidator
{
    public static bool IsValid(string phoneNumber)
    {
        var emailRegex = @"^(\+[0-9]{2,3}\s?)?\(?[0-9]{2}\)?\s?[0-9]{5}-?[0-9]{4}$";
        return Regex.IsMatch(phoneNumber, emailRegex);
    }
}