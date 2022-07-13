namespace Eshop.Product.Api.Services
{
    public class ProductService : IProductService
    {
        Task<ProductCreated> IProductService.AddProduct(CreateProduct createProduct)
        {
            throw null;
        }

        Task<ProductCreated> IProductService.GetProduct(Guid ProductId)
        {
            throw null;
        }
    }
}
