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
                Date = @event.Date,
                UserList = @event.UserList
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

        public bool Join(User user)
        {
            //if user is logged in
            //get user id
            //add to list
            Event @event = new Event();
            @event.UserList.Add(user);
            return true;
        }

        public void Update(Event @event)
        {
            _unitOfWork.EventRepository.Update(@event);
        }
    }
}
