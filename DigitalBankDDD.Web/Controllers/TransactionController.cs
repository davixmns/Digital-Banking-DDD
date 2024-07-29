using DigitalBankDDD.Application.Dtos;
using DigitalBankDDD.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace DigitalBankDDD.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class TransactionController : ControllerBase
{
    private readonly TransactionService _transactionService;
    
    public TransactionController(TransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTransactionAsync(TransactionRequestDto transactionRequestDto)
    {
        var result = await _transactionService.CreateTransactionAsync(transactionRequestDto);

        return result.IsSuccess
            ? Ok(result)
            : BadRequest(result);
    }
}