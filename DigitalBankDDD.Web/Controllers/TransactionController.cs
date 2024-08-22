using DigitalBankDDD.Application.Commands;
using DigitalBankDDD.Application.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DigitalBankDDD.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class TransactionController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public TransactionController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTransactionAsync(TransactionRequestDto transactionRequestDto)
    {
        var command = new CreateTransactionCommand(transactionRequestDto);
        
        var result = await _mediator.Send(command);

        return result.IsSuccess
            ? Ok(result)
            : BadRequest(result);
    }
}