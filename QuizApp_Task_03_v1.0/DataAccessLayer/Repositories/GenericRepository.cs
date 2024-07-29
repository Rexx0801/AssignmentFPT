using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;

namespace NWEC.P.L001_Task3.DataAccessLayer.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly QuizAppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(QuizAppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public IEnumerable<T> GetAll() => _dbSet.ToList();
        public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();
        public T GetById(Guid id) => _dbSet.Find(id);
        public async Task<T> GetByIdAsync(Guid id) => await _dbSet.FindAsync(id);
        public void Add(T entity) => _dbSet.Add(entity);
        public void Update(T entity) => _dbSet.Update(entity);
        public void Delete(Guid id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null) _dbSet.Remove(entity);
        }
        public void Delete(T entity) => _dbSet.Remove(entity);
        public IQueryable<T> GetQuery() => _dbSet.AsQueryable();
        public IQueryable<T> GetQuery(Expression<Func<T, bool>> predicate) => _dbSet.Where(predicate);
        public IQueryable<T> Get(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string includeProperties = "")
        {
            IQueryable<T> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            if (orderBy != null)
            {
                return orderBy(query);
            }

            return query;
        }
    }
}
