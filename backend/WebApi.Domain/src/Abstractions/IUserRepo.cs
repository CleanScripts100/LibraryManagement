using WebApi.Domain.src.Entities;

namespace WebApi.Domain.src.Abstractions;

public interface IUserRepo : IBaseRepository<User>
{
    Task<User> CreateAdmin(User user);
    Task<User> UpdatePassword(User user);
    Task<User?> FindOneByEmail(string email);
}