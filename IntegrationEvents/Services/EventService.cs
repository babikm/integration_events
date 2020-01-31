using Abstract;
using Abstract.DTO;
using Dal;
using Dal.Model;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class EventService : ServiceBase, IEventService
    {
        public EventService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public void Add(Event @event)
        {
            _unitOfWork.EventRepository.Create(new Event
            {
                Id = @event.Id,
                Name = @event.Name,
                Description = @event.Description,
                Spot = @event.Spot,
                Date = @event.Date,
                UserList = new List<User>()
            });
        }

        public void Delete(string id)
        {
            var @event = _unitOfWork.EventRepository.Get(id);
            foreach (var item in @event.UserList)
            {
                var user = _unitOfWork.UserRepository.Get(item.Id);
                user.EventJoined.RemoveAll(x => x == id);
                user.EventCreated.RemoveAll(x => x == id);
                var update = Builders<User>
                    .Update
                        .Set(x => x.EventCreated, user.EventCreated)
                        .Set(x => x.EventJoined, user.EventJoined);
                    
                _unitOfWork.UserRepository.Update(x => x.Id == user.Id, update);
            }
            _unitOfWork.EventRepository.Delete(id);
        }

        public Event Get(string id)
        {
            return _unitOfWork.EventRepository.Get(id);
        }

        public IEnumerable<EventDto> GetEvents()
        {
            return _unitOfWork.EventRepository.GetAll().Select(x => new EventDto
            {
               Id = x.Id,
               Name = x.Name,
               Date = x.Date,
               Description = x.Description,
               UserList = x.UserList
            }).ToList();
        }

        public void Update(Event @event, string id)
        {
            _unitOfWork.EventRepository.Update(x => x.Id == id, @event);
        }
    }
}
