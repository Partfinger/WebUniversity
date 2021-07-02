using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUniversity.Repository;

namespace WebUniversity.DataAccess
{
    public interface IUnitOfWork : IDisposable
    {
        TRepository GetRepository<TEntity, TRepository>()
            where TEntity : class
            where TRepository : IRepository<TEntity>, new();
        //void Update<TEntity>(TEntity entityToUpdate) where TEntity : class;
        void Save();
    }
}
