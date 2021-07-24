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
        UniversityContext db;
        readonly IDictionary<Type, object> repositoriesFactory;

        public UnitOfWork(UniversityContext db)
        {
            this.db = db;
            repositoriesFactory = new Dictionary<Type, object>();
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            Type key = typeof(TEntity);

            // add repo, lazy loading
            if (!repositoriesFactory.ContainsKey(key))
            {
                IRepository<TEntity> repository = new GenericRepository<TEntity>(db);

                repositoriesFactory.Add(key, repository);
            }

            // return repository
            return (IRepository<TEntity>)repositoriesFactory[key];
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

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
