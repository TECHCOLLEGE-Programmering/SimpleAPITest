using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface ICustomService<T> where T : class
    {
            IEnumerable<T> GetAll();
            T Get(int id);
            T Insert(T entity);
            T Update(T entity);
            void Delete(int id);
            //void Remove(T entity);
    }
}
