using DigitalBankDDD.Application.Interfaces;
using DigitalBankDDD.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DigitalBankDDD.Web.Controllers;

[Route("api/[controller]")]
public sealed class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAccount([FromBody] Account newAccount)
    {
        var result = await _accountService.CreateAccountAsync(newAccount);

        return result.IsSuccess
            ? StatusCode(StatusCodes.Status201Created, result.Data)
            : BadRequest(result.ErrorMessage);
    }
}