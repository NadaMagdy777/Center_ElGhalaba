namespace Center_ElGhlaba.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        void Insert(T Entity);
        void Update(T Entity);
        void Delete(T Entity);

    }
}
