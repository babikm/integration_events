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

        public void Delete(int id)
        {
            _collection.DeleteOneAsync(Builders<T>.Filter.Eq("Id", id));
        }

        public async Task<T> Get(int id)
        {
            var data = await _collection.FindAsync(Builders<T>.Filter.Eq("Id", id));
            return data.FirstOrDefault();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            var all = (await _collection.FindAsync(_ => true)).ToList();
            return all;
        }

        public void Update(T @event)
        {
            throw new NotImplementedException();
        }

    }
}
