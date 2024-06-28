namespace webtimesheet.DAL.Repositories
{
    public interface IRepositroy<T> where T: class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task Add(T entity);
        Task<T> Update(T entity);
        Task Delete(int id);
    }
}
