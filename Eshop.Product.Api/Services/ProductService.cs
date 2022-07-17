using Eshop.Product.Api.Repositories;

namespace Eshop.Product.Api.Services
{
    public class ProductService : IProductService
    { 
        private IProductRepository _repository { get; }
        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }
       
        public async Task<ProductCreated> AddProduct(CreateProduct product)
        {
            return await _repository.AddProduct(product);
        }

        public async Task<ProductCreated> GetProduct(string ProductId)
        {
            var product = await _repository.GetProduct(ProductId);
            return product;
        }
    }
}
