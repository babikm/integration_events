using Dal.Model;
using MongoDB.Driver;
using System.Configuration;
using System.Collections.Generic;
using System.Text;

namespace Dal
{
    public class UnitOfWork : IUnitOfWork
    {
        private IMongoDatabase _database;       

        public UnitOfWork()
        {
            var client = new MongoClient();
            var dbName = "IntegrationEventsDb";
            var conStr = ConfigurationManager.GetSection("ConnectionString");
            _database = client.GetDatabase(dbName);
        }

        private IRepository<Event> _eventRepository;
        public IRepository<Event> EventRepository
            => _eventRepository ?? (_eventRepository = new Repository<Event>(_database, "Events"));

        private IRepository<User> _userRepository;
        public IRepository<User> UserRepository
            => _userRepository ?? (_userRepository = new Repository<User>(_database, "Users"));

    }
}
