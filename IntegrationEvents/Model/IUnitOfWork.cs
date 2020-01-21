using Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dal
{
    public interface IUnitOfWork
    {
        IRepository<Event> EventRepository { get; }
        IRepository<User> UserRepository { get; }

    }
}
