  
using System.Collections.Generic;
using MongoDB.Driver;
using SoftRes.Models;

namespace SoftRes.SharpMongo
{
    public interface IMongoFactory
    {
        MongoService<Item> Item { get; }
    }
}