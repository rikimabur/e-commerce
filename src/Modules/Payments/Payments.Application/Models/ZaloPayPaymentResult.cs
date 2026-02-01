namespace Payments.Application.Models;
public sealed class ZaloPayPaymentResult
{
    public bool IsSuccess { get; init; }

    public string? TransactionId { get; init; }

    public string? ErrorCode { get; init; }

    public string? ErrorMessage { get; init; }

    public static ZaloPayPaymentResult Success(string txnId)
        => new()
        {
            IsSuccess = true,
            TransactionId = txnId
        };

    public static ZaloPayPaymentResult Failed(string code, string message)
        => new()
        {
            IsSuccess = false,
            ErrorCode = code,
            ErrorMessage = message
        };
}