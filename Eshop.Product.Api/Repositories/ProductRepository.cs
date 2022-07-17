using Eshop.Product.Api.Services;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

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
        public async Task<ProductCreated> AddProduct(CreateProduct product)
        {
            await _collection.InsertOneAsync(product);
            return new ProductCreated
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                CreatedAt = DateTime.UtcNow
            };
        }

        public async Task<ProductCreated> GetProduct(string ProductId)
        {
            var product = new CreateProduct();
            product = await _collection.AsQueryable().FirstOrDefaultAsync(
                p => p.ProductId == ProductId);
            return new ProductCreated() 
            { 
                ProductId = product.ProductId, 
                ProductName = product.ProductName 
            };
        }
    }
}
