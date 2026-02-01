using Microsoft.Extensions.Logging;
using Polly;

namespace Payments.Infrastructure.Payments.ZaloPay;
public static class ZaloPayPollyPolicies
{
    public static IAsyncPolicy<HttpResponseMessage> Create(
        ILogger logger)
    {
        return Policy<HttpResponseMessage>
            .Handle<HttpRequestException>()
            .OrResult(r => !r.IsSuccessStatusCode)
            .WaitAndRetryAsync(
                retryCount: 3,
                sleepDurationProvider: retry =>
                    TimeSpan.FromSeconds(Math.Pow(2, retry)),
                onRetry: (outcome, delay, retry, _) =>
                {
                    logger.LogWarning("ZaloPay retry {Retry} after {Delay}s. Status: {Status}",
                        retry,
                        delay.TotalSeconds,
                        outcome.Result?.StatusCode);
                })
            .WrapAsync(
                Policy<HttpResponseMessage>
                    .Handle<HttpRequestException>()
                    .CircuitBreakerAsync(
                        handledEventsAllowedBeforeBreaking: 5,
                        durationOfBreak: TimeSpan.FromSeconds(30)));
    }
}