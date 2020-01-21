using Abstract;
using Abstract.DTO;
using Dal;
using Dal.Model;
using System;
using System.Collections.Generic;
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

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Event Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<EventDto>> GetEvents()
        {
            var all = _unitOfWork.EventRepository.GetAll().Select(x => new EventDto):
            return all.ToList();
        }

        public void Update(Event @event)
        {
            throw new NotImplementedException();
        }
    }
}
