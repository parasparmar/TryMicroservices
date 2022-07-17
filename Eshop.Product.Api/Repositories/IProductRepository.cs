using Eshop.Product.Api.Services;

namespace Eshop.Product.Api.Repositories
{
    public interface IProductRepository
    {
        public  Task<ProductCreated> GetProduct(string ProductId);
        public  Task<ProductCreated> AddProduct(CreateProduct product);

    }
}
