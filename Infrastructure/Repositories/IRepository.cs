using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public interface IRepository<T> where T : class
    {
        T Insert(T entity);
        Task<T> InsertAsync(T entity);
        Task<List<T>> BulkInsertAsync(List<T> entity);
        void InsertRange(IEnumerable<T> entities);
        T GetByID(object ID);
        IEnumerable<T> GetAll();
        void Update(T entity);
        Task UpdateAsync(T entity);
        void Delete(T entity);
        void Delete(object ID);
        void DeleteRange(IEnumerable<T> entities);
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
    }
}
