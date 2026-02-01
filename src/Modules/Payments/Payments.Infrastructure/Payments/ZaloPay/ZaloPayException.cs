using System.Net;

namespace Payments.Infrastructure.Payments.ZaloPay;
public sealed class ZaloPayException : Exception
{
    public HttpStatusCode StatusCode { get; }
    public string? ProviderError { get; }

    public ZaloPayException(
        HttpStatusCode statusCode,
        string message)
        : base(message)
    {
        StatusCode = statusCode;
        ProviderError = message;
    }
}