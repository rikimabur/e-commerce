using BuildingBlocks.API.Modules;
using MassTransit;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Payments.Application.Abstractions;
using Payments.Application.Factories;
using Payments.Application.Gateways;
using Payments.Application.Providers;
using Payments.Application.Sagas;
using Payments.Infrastructure.Messaging.Consumers;
using Payments.Infrastructure.Payments.ZaloPay;
using Payments.Infrastructure.Persistence;
namespace Payments.Presentation;
public class PaymentsModule : IModule
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        throw new NotImplementedException();
    }

    public void Register(IServiceCollection services, IConfiguration configuration)
    {

        services.AddDbContext<PaymentsDbContext>(opt => opt.UseNpgsql(configuration.GetConnectionString("PaymentsDb")));


        services.AddScoped<IPaymentProvider, StripePaymentProvider>();
        services.AddScoped<IPaymentProvider, PayPalPaymentProvider>();
        services.AddScoped<IPaymentProvider, ZaloPayPaymentProvider>();

        services.AddSingleton<PaymentProviderFactory>();

        services.AddMassTransit(x =>
        {
            x.AddSagaStateMachine<PaymentSaga, PaymentSagaState>()
             .EntityFrameworkRepository(r =>
             {
                 r.UsePostgres();
                 r.ConcurrencyMode = ConcurrencyMode.Pessimistic;
             });
            x.AddConsumer<ChargePaymentConsumer>();

            x.UsingRabbitMq((context, rabbit) =>
            {
                rabbit.Host("rabbitmq", "/", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                rabbit.ReceiveEndpoint("charge-payment", endpoint =>
                {
                    endpoint.ConfigureConsumer<ChargePaymentConsumer>(context);

                    endpoint.PrefetchCount = 16;
                    endpoint.ConcurrentMessageLimit = 8;
                });
            });
        });

        services.Configure<ZaloPayOptions>(configuration.GetSection("ZaloPay"));

        services.AddHttpClient<IZaloPayGateway, ZaloPayClient>((sp, client) =>
        {
            var options = sp.GetRequiredService<IOptions<ZaloPayOptions>>().Value;
            client.Timeout = TimeSpan.FromSeconds(options.TimeoutSeconds);
        })
        .AddPolicyHandler((sp, _) =>
        {
            var logger = sp.GetRequiredService<ILogger<ZaloPayClient>>();
            return ZaloPayPollyPolicies.Create(logger);
        });
    }
}