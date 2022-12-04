using System.Linq.Expressions;

namespace WEBAPP.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);
        IEnumerable<T> GetAllData(string? includeProperties = null, Expression<Func<T, bool>>? filter=null);
        T GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProperties = null);

    }
}
