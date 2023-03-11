using Center_ElGhlaba.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

        public async Task<T> GetByIdAsync(int id)
        {

            return await _context.Set<T>().FindAsync(id);
        }
        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }
        public void Insert(T Entity)
        {

            _context.Set<T>().Add(Entity);
            _context.SaveChanges();
        }
        public void Update(T Entity)
        {
             _context.Update(Entity);


        }
        public void Delete(T Entity)
        {
            _context.Set<T>().Remove(Entity);
        }
    }

}
