using MassTransit;
using Payments.Contracts.Commands;

namespace Payments.Infrastructure.Messaging.Consumers;
public sealed class ChargePaymentConsumer : IConsumer<ChargePaymentCommand>
{
    private readonly ChargePaymentCommandHandler _handler;
    private readonly InboxStore _inbox;

    public ChargePaymentConsumer(ChargePaymentCommandHandler handler, InboxStore inbox)
    {
        _handler = handler;
        _inbox = inbox;
    }

    public async Task Consume(
        ConsumeContext<ChargePaymentCommand> context)
    {
        var messageId = context.MessageId;

        // MessageId MUST exist
        if (!messageId.HasValue)
            throw new InvalidOperationException("MessageId is required");

        // Deduplication
        if (await _inbox.ExistsAsync(messageId.Value, context.CancellationToken))
        {
            return; // already processed
        }

        // Handle command
        await _handler.Handle(
            context.Message,
            context.CancellationToken);

    }
}