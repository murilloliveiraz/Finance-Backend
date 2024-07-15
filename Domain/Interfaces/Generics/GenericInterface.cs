namespace Domain.Interfaces.Generics
{
    public interface GenericInterface<T> where T : class
    {
        Task Add(T entity);
        Task Update(T entity);
        Task Delete(T entity);
        Task<T> GetEntityById(long id);
        Task<IEnumerable<T>> Get();
    }
}
