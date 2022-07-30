using Eshop.ApiGateway.Controllers;
using MassTransit;

namespace EShop.Product.Api
{
    public class CreateProductHandler : IConsumer<CreateProduct>
    {        
        public async Task Consume(ConsumeContext<CreateProduct> context)
        {
            //using (var session = await _client.StartSessionAsync())
            //{
            //    try
            //    {
            //        session.StartTransaction();
            //        var isNew = await _service.IsNewMessage(context.MessageId);

            //        if (isNew)
            //        {
            //            await _service.AddProduct(context.Message);
            //            await _service.AddMessage(nameof(CreateProductHandler), context.MessageId);
            //            await Task.CompletedTask;
            //        }
            //        await session.CommitTransactionAsync();
            //    }
            //    catch (System.Exception)
            //    {
            //        await session.AbortTransactionAsync();
            //    }
            //}
        }
    }
}
