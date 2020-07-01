using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MongoDB.Bson;
using MongoDB.Driver;

namespace SoftRes.SharpMongo
{
    public interface IObjectId
    {
        ObjectId Id { get; set; }
    }

    public interface IMongoService<TModel, TIndex>
    {
        IEnumerable<TModel> Find(Expression<Func<TModel, bool>> predicate);
        List<TModel> Get();
        TModel Get(TIndex id);
        TModel Get(ObjectId id);
        TModel Create(TModel t);
        void CreateMany(List<TModel> t);
        void Update(TModel t);
        void Update(TModel t, TIndex id);
        void Remove(TModel t);
        void Remove(TIndex id);
        void RemoveMany(IEnumerable<TModel> t);
        void RemoveMany(IEnumerable<TIndex> ids);
        void RemoveMany(IEnumerable<ObjectId> ids);

        /// <summary>
        /// Run an arbitrary query on the underlying IMongoCollection.
        /// </summary>
        TResult Query<TResult>(Func<IMongoCollection<TModel>, TResult> query);
    }

    public class MongoService<TType> : IMongoService<TType, ObjectId>
        where TType: IObjectId 
    {
        protected IMongoCollection<TType> _collection;
        protected IMongoDatabase _database;
        public IMongoCollection<TType> All => _collection;
        private string _collectionName { get; set; }
        private string _databaseName { get; set; }

        public TResult Query<TResult>(Func<IMongoCollection<TType>, TResult> query) => query(_collection);

        public MongoService(IMongoClient client, string databaseName, bool capped = false, List<CreateIndexModel<TType>> indexes = null) 
        {
            _databaseName = databaseName;
            _collectionName = $"{typeof(TType).ToString().Split('.').Last().ToLower()}s";
            _database = client.GetDatabase(_databaseName);
            if (capped && !CollectionExists())
                CreateCappedCollection();
            _collection = _database.GetCollection<TType>(_collectionName);

            if (indexes != null) 
            {
                All.Indexes.CreateMany(indexes);
            }
        }

        public IEnumerable<TType> Find(Expression<Func<TType, bool>> predicate)
        {
            return _collection.Find(predicate).ToEnumerable();
        }

        public List<TType> Get()
        {
            return _collection.Find<TType>(t => true).ToList();
        }

        public TType Get(ObjectId id)
        {
            return _collection.Find<TType>(t => t.Id.Equals(id)).FirstOrDefault();

        }

        public TType Create(TType t)
        {
            _collection.InsertOne(t);

            return t;
        }

        public void CreateMany(List<TType> t)
        {
            _collection.InsertMany(t);
        }

        public void Update(TType type)
        {
            _collection.ReplaceOne(t => t.Id.Equals(type.Id), type);
        }

        public void Update(TType type, ObjectId id)
        {
            type.Id = id;
            _collection.ReplaceOne(t => t.Id.Equals(id), type);
        }

        public void Remove(TType type)
        {
            _collection.DeleteOne(t => t.Id.Equals(type.Id));
        }

        public void Remove(ObjectId id)
        {
            _collection.DeleteOne(t => t.Id.Equals(id));
        }
        // Overloads to allow any IEnumerable to be used with RemoveMany.
        public void RemoveMany(IEnumerable<ObjectId> ids)
        {
            var hashSet = ids.ToHashSet();
            RemoveMany(hashSet);
        }

        public void RemoveMany(IEnumerable<TType> types)
        {
            var hashSet = types.Select(x => x.Id).ToHashSet();
            RemoveMany(hashSet);
        }
        // Method doing the actual deleting.
        private void RemoveMany(HashSet<ObjectId> ids)
        {
            _collection.DeleteMany(x => ids.Contains(x.Id));
        }

        private void CreateCappedCollection()
        {
            _database.CreateCollection(_collectionName, new CreateCollectionOptions
            {
                // TODO: Optimize this. Spitballing values here.
                Capped = true,
                MaxSize = 5000,
                MaxDocuments = 1000
            });
        }

        private bool CollectionExists()
        {
            var filter = new BsonDocument("name", _collectionName);
            var options = new ListCollectionNamesOptions { Filter = filter };

            return _database.ListCollectionNames(options).Any();
        }
    }
}