namespace Eshop.Product.Api.Services
{
    public interface IProductService
    {
        Task<ProductCreated> GetProduct(Guid ProductId);
        Task<ProductCreated> AddProduct(CreateProduct createProduct);
    }
}
