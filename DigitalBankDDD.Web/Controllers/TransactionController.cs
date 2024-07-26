using DigitalBankDDD.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace DigitalBankDDD.Web.Controllers;

[Route("api/[controller]")]
public sealed class TransactionController : ControllerBase
{
    private readonly TransactionService _transactionService;

    public TransactionController()
    {
        
    }
}