using Center_ElGhalaba.Constants;
using Center_ElGhlaba.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Linq.Expressions;
using UserIdentity.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Center_ElGhlaba.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected ApplicationDbContext _context;
        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<T>> GetAllAsync(string[] includes = null)
        {
            IQueryable<T> query = _context.Set<T>();

            if(includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return await query.ToListAsync();
        }
        public async Task<T> GetByIdAsync(int id)
        {

            return await _context.Set<T>().FindAsync(id);
        }
        public async Task<T> FindAsync(Expression<Func<T, bool>> criteria, string[] includes = null)
        {
            IQueryable<T> query = _context.Set<T>();

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
                
            }
            return await query.SingleOrDefaultAsync(criteria);
        }
        public async Task<List<T>> FindAllAsync(Expression<Func<T, bool>> criteria, string[] includes = null,
            Expression<Func<T, object>> orderBy = null, string orderByDirection = OrderBy.Ascending)
        {
            IQueryable<T> query = _context.Set<T>();
            query = query.Where(criteria);

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
                query = query.Where(criteria);
            }

            if(orderBy != null)
            {
                if(orderByDirection == OrderBy.Ascending)
                {
                    query = query.OrderBy(orderBy);
                }
                else
                {
                    query = query.OrderByDescending(orderBy);
                }
            }
            return await query.ToListAsync();
        }
        public void Insert(T Entity)
        {
            _context.Set<T>().Add(Entity);
        }
        public void Update(T Entity)
        {
             _context.Update(Entity);
        }
        public void Delete(T Entity)
        {
            _context.Set<T>().Remove(Entity);
        }
        public async Task<int> CountAsync()
        {
            return await _context.Set<T>().CountAsync();
        }
        public async Task<int> CountAsync(Expression<Func<T, bool>> criteria)
        {
            return await _context.Set<T>().CountAsync(criteria);
        }

       
    }

}
