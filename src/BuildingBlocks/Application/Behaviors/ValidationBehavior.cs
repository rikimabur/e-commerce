using FluentValidation;
using MediatR;

namespace BuildingBlocks.Application.Validation;
public class ValidationBehavior<TReq, TRes>: IPipelineBehavior<TReq, TRes>
{
    private readonly IEnumerable<IValidator<TReq>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TReq>> validators)
        => _validators = validators;

    public async Task<TRes> Handle(
        TReq request,
        RequestHandlerDelegate<TRes> next,
        CancellationToken ct)
    {
        if (_validators.Any())
        {
            var context = new ValidationContext<TReq>(request);
            var failures = _validators
                .Select(v => v.Validate(context))
                .SelectMany(r => r.Errors)
                .Where(f => f != null)
                .ToList();

            if (failures.Count != 0)
                throw new ValidationException(failures);
        }

        return await next(ct);
    }
}