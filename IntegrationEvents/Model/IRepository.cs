using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public interface IRepository<T> where T : class
    {
        Task<T> Get(int id);
        Task<IEnumerable<T>> GetAll();
        void Create(T @event);
        void Update(T @event);
        void Delete(T @event);
        void Delete(int id);

    }
}
