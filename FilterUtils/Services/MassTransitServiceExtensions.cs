using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using WebApplication1;

namespace Utils.Services
{
    public static class MassTransitServiceExtensions
    {
        public static void AddMassTransitServicesT<T>(this IServiceCollection services, string host, string username, string password, string queueName)
            where T : class, IConsumer
        {
            services.AddScoped<MessageProducer>();
            services.AddMassTransit(x =>
            {
                x.AddConsumer<T>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(host, "/", h =>
                    {
                        h.Username(username);
                        h.Password(password);
                    });

                    cfg.ReceiveEndpoint(queueName, e =>
                    {
                        e.ConfigureConsumer<T>(context);
                    });
                });
            });

            services.AddMassTransitHostedService();
        }
    }
}
