using System.Configuration;
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

            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IProductService, ProductService>();
            //builder.Services.AddScoped<CreateProductHandler>();
                        
            #region-- MongoDb Config--            
            builder.Services.AddMongoDb(builder.Configuration);
            #endregion

            #region-- RabbitMq Config--
            //var rabbitMqOption = new RabbitMQOption();
            //configuration.GetSection("rabbitmq").Bind(rabbitMqOption);
            //builder.Services.AddRabbitMq(configuration.GetSection("rabbitmq"));
            #endregion

            #region-- Mass Transit Eventbus with RabbitMq Config--
            //builder.Services.AddMassTransit(mt =>
            //{
            //    mt.AddMassTransit(mt =>
            //    {
            //        //mt.AddConsumer<CreateProduct>();
            //        mt.AddBus(provider =>
            //        Bus.Factory.CreateUsingRabbitMq(rmqCfg =>
            //        {
            //            rmqCfg.Host(new Uri(rabbitMqOption.ConnectionString), rmqHost =>
            //                        {
            //                            rmqHost.Username(rabbitMqOption.Username);
            //                            rmqHost.Password(rabbitMqOption.Password);
            //                        });
            //            rmqCfg.ReceiveEndpoint("create_product", ep =>
            //            {
            //                ep.PrefetchCount = 16;
            //                ep.UseMessageRetry(rtrycfg => { rtrycfg.Interval(2, 100); });
            //                //ep.ConfigureConsumer<CreateProduct>(provider);
            //            });
            //        }));
            //    });
            //});
            #endregion

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}