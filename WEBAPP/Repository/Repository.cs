using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WEBAPP.Data;
using WEBAPP.Repository.IRepository;

namespace WEBAPP.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        DbSet<T> dbset;

        public Repository(ApplicationDbContext db) 
        {
            _db = db;
           this.dbset = _db.Set<T>();
        }
        public void Add(T entity)
        {
            dbset.Add(entity);
        }

        public IEnumerable<T> GetAllData(string? includeProperties=null, Expression<Func<T, bool>>? filter=null)
        {
            IQueryable<T> query = dbset;
            
            if (includeProperties != "")
            {
                query = query.Include(includeProperties);
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }


            return query.ToList();
        }

        public T GetFirstOrDefault(Expression < Func<T, bool>> filter, string? includeProperties = null)
        {
            IQueryable<T> query = dbset;
            if (includeProperties != null)
            {
                query = query.Include(includeProperties);
            }
            query = query.Where(filter);
            
            return query.FirstOrDefault();
        }

        public void Remove(T entity)
        {
            dbset.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            dbset.RemoveRange(entity);
        }
    }
}
