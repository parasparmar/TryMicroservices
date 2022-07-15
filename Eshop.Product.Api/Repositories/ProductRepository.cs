using Eshop.Product.Api.Services;
using MongoDB.Driver;

namespace Eshop.Product.Api.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<CreateProduct> _collection;

        public ProductRepository(IMongoDatabase database)
        {
            _database = database;
            _collection = database.GetCollection<CreateProduct>("product");
        }
        Task<ProductCreated> IProductRepository.AddProduct(CreateProduct product)
        {
            throw new NotImplementedException();
        }

        Task<ProductCreated> IProductRepository.GetProduct(Guid ProductId)
        {
            throw new NotImplementedException();
        }
    }
}
