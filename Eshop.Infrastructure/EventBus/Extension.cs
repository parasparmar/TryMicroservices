using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Eshop.Infrastructure.EventBus
{
    public static class Extension
    {
        public static IServiceCollection AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
        {
            var rabbitMq = new RabbitMQOption();
            configuration.GetSection("rabbitmq").Bind(rabbitMq);
            // establish connection with rabbitMQ..
            services.AddMassTransit(x => {
                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    cfg.Host(new Uri(rabbitMq.ConnectionString), hostcfg => {
                        hostcfg.Username(rabbitMq.Username);
                        hostcfg.Password(rabbitMq.Password);
                    });
                    cfg.ConfigureEndpoints(provider);
                }));                
            });

            //TODO: Locate this deprecated service dependency
            //services.AddMassTransitHostedService();
            return services;

        }
    }

}
