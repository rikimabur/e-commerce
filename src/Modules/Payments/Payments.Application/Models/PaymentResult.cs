namespace Payments.Application.Models;
public sealed record PaymentResult(
    bool Success,
    string Provider,
    string? TransactionId,
    string? ErrorMessage
);
