using Dal.Model;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
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

        public void Create(T entity)
        {
            _collection.InsertOne(entity);
        }

        public void Delete(T entity)
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

        public T Update(Expression<Func<T, bool>> filter, T replacement)
        {
            return _collection.FindOneAndReplace(filter, replacement);
        }

        public async Task<T> Update(FilterDefinition<T> filter, T replacement)
        {
            return await _collection.FindOneAndReplaceAsync(filter, replacement);

        }

        public T GetByUsername(string username)
        {
            var user = _collection.Find(Builders<T>.Filter.Eq("UserName", username));
            return user.FirstOrDefault();
        }

    }
}
