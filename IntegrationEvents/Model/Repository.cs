using Dal.Model;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private IMongoDatabase _database;
        private string _collectionName;
        private IMongoCollection<T> _collection;

        public Repository(IMongoDatabase database, string collectionName)
        {
            _database = database;
            _collectionName = collectionName;
            _collection = database.GetCollection<T>(collectionName);
        }

        public void Create(T @event)
        {
            _collection.InsertOne(@event);
        }

        public void Delete(T @event)
        {
            throw new NotImplementedException();
        }

        public void Delete(string id)
        {
            _collection.DeleteOneAsync(Builders<T>.Filter.Eq("Id", id));
        }

        public T Get(string id)
        {
            var data = _collection.FindSync(Builders<T>.Filter.Eq("Id", id));
            return data.FirstOrDefault();
        }

        public  IEnumerable<T> GetAll()
        {
            var all =  _collection.FindSync(_ => true).ToList();
            return all;
        }

        public void Update(T @event)
        {
            //_collection.ReplaceOne(x=>x.Id==)
        }

    }
}
