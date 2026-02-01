using BuildingBlocks.Infrastructure.Outbox;
using MassTransit;
using Orders.Contracts.Events;

namespace Orders.Application.Consumer;
//public sealed class OrderCreatedConsumer : IConsumer<OrderCreatedIntegrationEvent>
//{
//    private readonly IInboxStore _inbox;

//    public OrderCreatedConsumer(IInboxStore inbox)
//    {
//        _inbox = inbox;
//    }

//    public async Task Consume(
//        ConsumeContext<OrderCreatedIntegrationEvent> context)
//    {
//        var messageId = context.MessageId!.Value;

//        if (await _inbox.ExistsAsync(messageId, context.CancellationToken))
//            return;

//        // ---- BUSINESS LOGIC ----
//        // create payment, reserve money, etc.

//        await _inbox.SaveAsync(messageId, context.CancellationToken);
//    }
//}