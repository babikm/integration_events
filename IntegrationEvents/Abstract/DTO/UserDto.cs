using Dal.Model;
using MongoDB.Bson;
using System.Collections.Generic;

namespace Abstract.DTO
{
    public class UserDto
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public List<Event> EventCreated { get; set; }
        public List<Event> EventJoined { get; set; }
    }
}
