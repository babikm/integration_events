using Dal.Model;
using System.Collections.Generic;

namespace Abstract.DTO
{
    public class UserDto
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public IList<Event> EventList { get; set; }

    }
}
