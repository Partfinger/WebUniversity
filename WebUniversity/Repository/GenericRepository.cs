using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUniversity.DataAccess;

namespace WebUniversity.Repository
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        UniversityContext db;

        public GenericRepository(UniversityContext context)
        {
            db = context;
        }

        public void Create(TEntity item)
        {
            db.Add(item);
        }

        public void Delete(int? id)
        {
            throw new NotImplementedException();
        }

        public void Delete(TEntity item)
        {
            throw new NotImplementedException();
        }

        public TEntity Find(int? id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TEntity> GetAll()
        {
            return db.Set<TEntity>();
        }

        public void Update(TEntity item)
        {
            throw new NotImplementedException();
        }
    }
}
