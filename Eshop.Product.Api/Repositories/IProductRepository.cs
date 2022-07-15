using Eshop.Product.Api.Services;

namespace Eshop.Product.Api.Repositories
{
    public interface IProductRepository
    {
        Task<ProductCreated> GetProduct(Guid ProductId);
        Task<ProductCreated> AddProduct(CreateProduct product);

    }
}
