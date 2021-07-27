using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;

namespace DataAccessLayer.EF
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

            if (!repositoriesFactory.ContainsKey(key))
            {
                IRepository<TEntity> repository = new Repository<TEntity>(db);

                repositoriesFactory.Add(key, repository);
            }

            return (IRepository<TEntity>)repositoriesFactory[key];
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
            db.SaveChanges();
        }
    }
}
