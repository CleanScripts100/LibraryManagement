using WebApi.Domain.src.Shared;

namespace WebApi.Domain.src.Abstractions
{
    public interface IBaseRepo<T> // repo should not work with Dto, but original entities
    {
        Task<IEnumerable<T>> GetAll(QueryOptions queryOptions); //should consider the sorting, searching, pagination
        Task<T?> GetOneById(Guid id);
        Task<T> UpdateOneById(T updatedEntity);
        Task<bool> DeleteOneById(T entity);
        Task<T> CreateOne(T entity);
    }
}