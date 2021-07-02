using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUniversity.Repository;

namespace WebUniversity.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        UniversityContext db = new UniversityContext();
        readonly IDictionary<Type, object> repositoriesFactory;

        public UnitOfWork()
        {
            repositoriesFactory = new Dictionary<Type, object>();
        }

        public TRepository GetRepository<TEntity, TRepository>()
            where TEntity : class
            where TRepository : IRepository<TEntity>, new()
        {
            Type key = typeof(TEntity);

            // add repo, lazy loading
            if (!repositoriesFactory.ContainsKey(key))
            {
                //TRepository repository = new GenericRepository<TEntity>(db);

                //repositoriesFactory.Add(key, repository);
            }

            // return repository
            return (TRepository)repositoriesFactory[key];
        }

        public void Save()
        {
            db.SaveChanges();
        }

        bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }
    }
}
