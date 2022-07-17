﻿using MongoDB.Bson.Serialization.Attributes;

namespace Eshop.Product.Api.Services
{
    public class CreateProduct
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public float ProductPrice { get; set; }
        public Guid CategoryId { get; set; }
    }
}