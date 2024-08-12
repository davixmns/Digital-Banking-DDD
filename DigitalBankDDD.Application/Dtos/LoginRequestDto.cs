namespace DigitalBankDDD.Application.Dtos;

public class LoginRequestDto
{
    public string EmailOrCpf { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
}