using WebApi.Domain.src.Shared;

namespace WebApi.Business.src.Abstractions
{
    public interface IBaseService<T, TReadDto, TCreateDto, TUpdateDto>
    {
        Task<IEnumerable<TReadDto>> GetAll(QueryOptions queryOptions);
        Task<TReadDto> GetOneById(Guid id);
        Task<TReadDto> UpdateOneById(Guid id, TUpdateDto updated);
        Task<bool> DeleteOneById(Guid id);
        Task<TReadDto> CreateOne(TCreateDto dto);
    }
}

/* create user: names, email, password
read 1 user: names, email, password, role
update user profile: names, email
update password
read 1 product: title, price, description, images
create product:  title, price, description, images, inventory*/