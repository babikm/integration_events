using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dal
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private IMongoDatabase _database;

        public void Create(T @event)
        {
            throw new NotImplementedException();
        }

        public void Delete(T @event)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public T Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(T @event)
        {
            throw new NotImplementedException();
        }
    }
}
