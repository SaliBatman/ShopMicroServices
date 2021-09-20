using Catalog.Api.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.Api.Data
{
    public class CatalogContex : ICatalogContext
    {

        public CatalogContex(IConfiguration configuration)
        {

            var connectionString = configuration.GetValue<string>("DatabaseSettings:ConnectionString");
            var databaseName = configuration.GetValue<string>("DatabaseSettings:DatabaseName");
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase(databaseName);
            ProductCollection = db.GetCollection<Product>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
            CatalogContextSeedData.SeedData(ProductCollection);

        }
        public IMongoCollection<Product> ProductCollection { get; }
    }
}
