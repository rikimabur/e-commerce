using BuildingBlocks.Infrastructure.Outbox;
using Microsoft.Extensions.Logging;
using Payments.Application.Factories;
using Payments.Application.Models;
using Payments.Contracts.Commands;
using System.Text.Json;

public sealed class ChargePaymentCommandHandler
{
    private readonly PaymentProviderFactory _providerFactory;
    private readonly IInboxStore _outbox;
    private readonly ILogger<ChargePaymentCommandHandler> _logger;

    public ChargePaymentCommandHandler(
        PaymentProviderFactory providerFactory,
        IInboxStore outbox,
        ILogger<ChargePaymentCommandHandler> logger)
    {
        _providerFactory = providerFactory;
        _outbox = outbox;
        _logger = logger;
    }

    public async Task Handle(
        ChargePaymentCommand command,
        CancellationToken ct)
    {
        _logger.LogInformation(
            "Charging payment for OrderId={OrderId}, Provider={Provider}",
            command.OrderId,
            command.PaymentProvider);

        var provider = _providerFactory.Resolve(command.PaymentProvider);

        var result = await provider.ChargeAsync(
            new PaymentRequest(
                command.OrderId,
                command.Amount,
                command.Currency),
            ct);
        var outbox = new OutboxMessage
        {
            Id = Guid.NewGuid(),
            Type = result.GetType().Name,
            Payload = JsonSerializer.Serialize(result),
            OccurredOn = DateTime.UtcNow
        };
        await _outbox.SaveAsync(outbox, ct);

    }
}