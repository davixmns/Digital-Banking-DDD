using DigitalBankDDD.Domain.Exceptions;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using ApplicationException = DigitalBankDDD.Application.Exceptions.ApplicationException;

namespace DigitalBankDDD.Application.PipelineBehaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, 
        CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);
        
        var failures = _validators
            .Select(v => v.ValidateAsync(context, cancellationToken))
            .SelectMany(result => result.Result.Errors)
            .Where(f => f != null)
            .ToList();

        if (failures.Any())
            throw new DomainException(failures.Select(f => f.ErrorMessage).FirstOrDefault()!);
        
        return await next();
    }
}