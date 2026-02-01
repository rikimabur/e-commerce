using Microsoft.Extensions.Options;
using Payments.Application.Gateways;
using Payments.Application.Models;
using System.Text;
using System.Text.Json;

namespace Payments.Infrastructure.Payments.ZaloPay;
public sealed class ZaloPayClient : IZaloPayGateway
{
    private readonly HttpClient _httpClient;
    private readonly ZaloPayOptions _options;

    public ZaloPayClient(
        HttpClient httpClient,
        IOptions<ZaloPayOptions> options)
    {
        _httpClient = httpClient;
        _options = options.Value;
    }

    public async Task<ZaloPayPaymentResult> PayAsync(
        ZaloPayPaymentRequest request,
        CancellationToken ct)
    {
        var json = JsonSerializer.Serialize(request);
        using var content = new StringContent(json, Encoding.UTF8, "application/json");

        // Idempotency key (OrderId)
        content.Headers.Add("Idempotency-Key", request.OrderId);

        var response = await _httpClient
            .PostAsync(_options.Endpoint, content, ct);

        var body = await response.Content.ReadAsStringAsync(ct);

        if (!response.IsSuccessStatusCode)
        {
            throw new ZaloPayException(
                response.StatusCode,
                body);
        }

        return JsonSerializer.Deserialize<ZaloPayPaymentResult>(body)!;
    }
}