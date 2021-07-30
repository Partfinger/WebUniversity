 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IRepository<T> where T: class
    {
        T Find(int? id);
        IQueryable<T> GetAll();
        void Create(T item);
        void Update(T item);
        void Delete(T item);
    }
}
