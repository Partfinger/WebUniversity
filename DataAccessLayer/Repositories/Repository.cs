using DataAccessLayer.EF;
using DataAccessLayer.Interfaces;
using System;
using System.Linq;

namespace DataAccessLayer.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        UniversityContext db;

        public Repository(UniversityContext context)
        {
            db = context;
        }

        public void Create(TEntity item)
        {
            db.Add(item);
        }

        public void Delete(TEntity item)
        {
            db.Set<TEntity>().Remove(item);
        }

        public TEntity Find(int? id)
        {
            return db.Set<TEntity>().Find(id);
        }

        public IQueryable<TEntity> GetAll()
        {
            return db.Set<TEntity>();
        }

        public void Update(TEntity item)
        {
            db.Set<TEntity>().Update(item);
        }
    }
}
