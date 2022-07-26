using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.ApiGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IBusControl _bus;
        public ProductController(IBusControl bus)
        {
            _bus = bus;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            await Task.CompletedTask;
            return Accepted("GET Product Method Called");
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromForm] CreateProduct product)
        {
            var uri = new Uri("rabbitmq://localhost/create_product");
            var endpoint = _bus.GetSendEndpoint(uri);
            await _bus.Send(endpoint);
            return Accepted("ProductCreated");
        }
    }
}
