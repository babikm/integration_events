using Dal.Model;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public interface IRepository<T> where T : class
    {
        T Get(string id);
        IEnumerable<T> GetAll();
        void Create(T entity);
        T Update(Expression<Func<T, bool>> filter, T replacement);
        Task<T> Update(FilterDefinition<T> filter, T replacement);
        T GetByUsername(string username);
        void Delete(T entity);
        void Delete(string id);

    }
}
