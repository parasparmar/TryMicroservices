namespace Eshop.Product.Api.Services
{
    public interface IProductService
    {
        Task<ProductCreated> GetProduct(string ProductId);
        Task<ProductCreated> AddProduct(CreateProduct createProduct);
    }
}
