using Eshop.Infrastructure.Mongo;
using Eshop.Product.Api.Repositories;
using Eshop.Product.Api.Services;
using Microsoft.Extensions.Configuration;
using Eshop.Infrastructure.EventBus;
using MassTransit;
using ConfigurationManager = Microsoft.Extensions.Configuration.ConfigurationManager;

namespace Eshop.Product.Api
{
    public class Program
    {

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.
            builder.Services.AddControllers();            
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            #region-- MongoDb Config--            
            IConfiguration ic = builder.Configuration;
            builder.Services.AddMongoDb(ic);
            #endregion

            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<CreateProductHandler>();

            #region-- RabbitMq Config--
            var rabbitMqOption = new RabbitMQOption();
            ic.GetSection("rabbitmq").Bind(rabbitMqOption);
            builder.Services.AddRabbitMq(ic.GetSection("rabbitmq"));
            #endregion

            #region-- Mass Transit Eventbus with RabbitMq Config--
            builder.Services.AddMassTransit(x =>
            {
                x.AddConsumer<CreateProductHandler>();
                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
                    {
                        cfg.Host(new Uri(rabbitMqOption.ConnectionString), hostconfig =>
                                    {
                                        hostconfig.Username(rabbitMqOption.Username);
                                        hostconfig.Password(rabbitMqOption.Password);
                                    });
                        cfg.ReceiveEndpoint("create_product", ep =>
                        {
                            ep.PrefetchCount = 16;
                            ep.UseMessageRetry(rtrycfg => { rtrycfg.Interval(2, 100); });
                            ep.ConfigureConsumer<CreateProductHandler>(provider);
                        });
                    }));
            });
            #endregion
            
            var app = builder.Build();
            var busControl = app.Services.GetService<IBusControl>();
            busControl.Start();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}