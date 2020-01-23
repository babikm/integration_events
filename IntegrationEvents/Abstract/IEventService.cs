using Abstract.DTO;
using Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Abstract
{
    public interface IEventService
    {
        IEnumerable<EventDto> GetEvents();
        Event Get(string id);
        void Add(Event @event);
        void Delete(string id);
        void Update(Event @event);
        bool Join(User user, Event @event);

    }
}
