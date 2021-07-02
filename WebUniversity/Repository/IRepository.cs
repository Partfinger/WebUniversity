 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUniversity.Repository
{
    public interface IRepository<T> where T: class
    {
        T Get(int? id);
        IEnumerable<T> GetAll();
        void Create(T item);
        void Update(T item);
        void Delete(int? id);
    }
}
