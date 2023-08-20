using WebApi.Domain.src.Shared;

namespace WebApi.Domain.src.Abstractions
{
    public interface IBaseRepository<T> 
    {
        Task<IEnumerable<T>> GetAll(QueryOptions queryOptions); 
        Task<T?> GetOneById(Guid id);
        Task<T> UpdateOneById(T updatedEntity);
        Task<bool> DeleteOneById(T entity);
        Task<T> CreateOne(T entity);
        Task<IEnumerable<T>> GetAll(); 
    }
}