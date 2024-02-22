using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Text;

namespace Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationDbContext db;

        public Repository(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<List<T>> BulkInsertAsync(List<T> entities)
        {
            await db.Set<T>().AddRangeAsync(entities);
            db.SaveChanges();
            return entities;
        }

        public async Task<List<T>> BulkUpdateAsync(List<T> entities)
        {
            db.Set<T>().UpdateRange(entities);
            await db.SaveChangesAsync();
            return entities;
        }

        public void Delete(T entity)
        {
            db.Set<T>().Remove(entity);
            db.SaveChanges();
        }

        public void Delete(object ID)
        {
            T entity = db.Set<T>().Find(ID);
            this.Delete(entity);
        }

        public async Task DeleteAsync(T entity)
        {
            db.Set<T>().Remove(entity);
            await db.SaveChangesAsync();
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            db.Set<T>().RemoveRange(entities);
            db.SaveChanges();
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return db.Set<T>().Where(predicate);
        }

        public IEnumerable<T> GetAll()
        {
            return db.Set<T>().ToList();
        }

        public T GetByID(object ID)
        {
            return db.Set<T>().Find(ID);
        }

        public T Insert(T entity)
        {
            try
            {
                db.Set<T>().Add(entity);
                db.SaveChanges();
                return entity;
            }
            catch (DbUpdateException ex)
            {
                string errMsg = GetFullErrorText(ex);
                throw new InvalidOperationException(errMsg);
                //throw new CustomException(errMsg, isHandled: false, statusCode: ErrorNotificationType.VALIDATIONFAILURE);
            }
        }

        public async Task<T> InsertAsync(T entity)
        {
            db.Set<T>().Add(entity);
            await db.SaveChangesAsync();
            return entity;
        }

        public void InsertRange(IEnumerable<T> entities)
        {
            db.Set<T>().AddRange(entities);
            db.SaveChanges();
        }

        public void Update(T entity)
        {
            try
            {
                db.Entry<T>(entity).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                string errMsg = GetFullErrorText(ex);
                //throw new CustomException(errMsg, isHandled: false, statusCode: ErrorNotificationType.VALIDATIONFAILURE);
            }
        }

        public async Task UpdateAsync(T entity)
        {
            db.Entry<T>(entity).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }

        private string GetFullErrorText(DbUpdateException exc)
        {
            StringBuilder msg = new StringBuilder();
            msg.AppendLine($"DbUpdateException error details - {exc?.InnerException?.InnerException?.Message}");

            foreach (var eve in exc.Entries)
            {
                msg.AppendLine($"Entity of type {eve.Entity.GetType().Name} in state {eve.State} could not be updated");
            }
            return msg.ToString();
        }
    }
}
