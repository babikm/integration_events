using MongoDB.Driver;
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
        void Create(T entity);
        void Update(T entity, string id);
        void Delete(T entity);
        void Delete(string id);

    }
}
