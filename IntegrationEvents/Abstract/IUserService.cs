using Abstract.DTO;
using Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abstract
{
    public interface IUserService
    {
        IEnumerable<UserDto> GetUsers();
        User Get(string id);
        void Add(User user);
        void Delete(string id);
        void Update(User user, string id);
    }
}
