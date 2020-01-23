using Abstract;
using Abstract.DTO;
using Dal;
using Dal.Model;
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
                Date = @event.Date
            });
        }

        public void Delete(string id)
        {
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
               Description = x.Description
            }).ToList();
        }

        public bool Join(User user, Event @event)
        {
            //if user is logged in
            var isJoined = @event.UserList.Contains(user);
            if(!isJoined)
            {
                @event.UserList.Add(user);
                user.EventList.Add(@event);
                return true;
            }
            return false;
        }

        public void Update(Event @event)
        {
            _unitOfWork.EventRepository.Update(new Event 
            {
                Id = @event.Id,
                Name = @event.Name,
                Description = @event.Description,
                Spot = @event.Spot,
                Date = @event.Date,
                UserList = @event.UserList
            });
        }
    }
}
