using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public interface IRepository<T> where T : class
    {
        T Get(string id);
        IEnumerable<T> GetAll();
        void Create(T @event);
        void Update(T @event);
        void Delete(T @event);
        void Delete(string id);

    }
}
