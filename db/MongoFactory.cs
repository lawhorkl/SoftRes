using System.Collections.Generic;
using MongoDB.Driver;
using SoftRes.Models;
using SoftRes.SharpMongo;

namespace SoftRes.db
{
    public class MongoFactory : IMongoFactory
    {
        private readonly string _databaseName;
        private readonly IMongoClient _client;

        public MongoFactory(IMongoClient client, string databaseName)
        {
            _client = client;
            _databaseName = databaseName;
        }

        public MongoService<Item> Item => GetService<Item>(_databaseName);

        public MongoService<TType> GetService<TType>(
            string databaseName,
            bool capped = false, 
            List<CreateIndexModel<TType>> indexes = null) 
                where TType : IObjectId =>
                    new MongoService<TType>(
                        _client, 
                        databaseName, 
                        capped, 
                        indexes
                    );
    }
}