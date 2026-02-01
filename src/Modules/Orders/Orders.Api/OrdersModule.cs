using BuildingBlocks.API.Modules;
using MassTransit;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Orders.Domain.Repositories;
using Orders.Infrastructure.Persistence;
using Orders.Infrastructure.Repositories;
namespace Orders.Presentation
{
    public class OrdersModule : IModule
    {
        public void MapEndpoints(IEndpointRouteBuilder app)
        {
            throw new NotImplementedException();
        }

        public void Register(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<OrdersDbContext>(opt =>
               opt.UseNpgsql(configuration.GetConnectionString("OrdersDb")));

            services.AddScoped<IOrderRepository, OrderRepository>();


            // Configure MassTransit with the Outbox
            services.AddMassTransit(x =>
            {
                //OPTIONAL: Add consumers here if Orders consumes events
                //     x.AddConsumer<SomeConsumer>();
                // Configure Entity Framework Core Outbox
                x.AddEntityFrameworkOutbox<OrdersDbContext>(o =>
                {
                    // Configure the Postgres Lock Statement
                    o.UsePostgres();

                    // Configure Outbox to use Postgres transport
                    o.UseBusOutbox();

                    // Query for ready messages every 5 seconds
                    o.QueryDelay = TimeSpan.FromSeconds(5);

                    // Optional: Further configuration
                    o.DuplicateDetectionWindow = TimeSpan.FromMinutes(30);
                });
                // Configure RabbitMQ (or your preferred transport)
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("localhost");
                    //cfg.Host("rabbitmq", "/", h =>
                    //{
                    //    h.Username("guest");
                    //    h.Password("guest");
                    //});

                    cfg.ConfigureEndpoints(context);
                });
            });
        }
    }
}
