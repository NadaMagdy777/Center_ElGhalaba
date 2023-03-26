using Center_ElGhalaba.Constants;
using System.Linq.Expressions;

namespace Center_ElGhlaba.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync(string [] includes = null);
        Task<T> GetByIdAsync(int id);
        Task<T> FindAsync(Expression<Func<T, bool>> criteria, string[] includes = null);
        Task<List<T>> FindAllAsync(Expression<Func<T, bool>> criteria, string[] includes = null, 
            Expression<Func<T, object>> orderBy = null, string orderByDirection = OrderBy.Ascending);
        void Insert(T Entity);
        void Update(T Entity);
        void Delete(T Entity);

        Task<int> CountAsync();
        Task<int> CountAsync(Expression<Func<T, bool>> criteria);

    }
}
