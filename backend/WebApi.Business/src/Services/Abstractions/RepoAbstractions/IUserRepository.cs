using WebApi.Domain.src.Entities;

namespace WebApi.Business.src.Services.Abstractions.RepoAbstractions
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAllUsers();
        User GetUserById(Guid Id);
        User AddUser(User User);
        User AddAdmin(User user);
        User UpdateUser(Guid Id, User User);
        User UpdateAdmin(Guid Id, User User);
        String DeleteUser(Guid Id);
    }
}