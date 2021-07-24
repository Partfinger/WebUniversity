using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        //IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
        void SaveChanges();
    }
}
