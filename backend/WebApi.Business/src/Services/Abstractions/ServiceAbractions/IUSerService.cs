using WebApi.Business.src.Dto;

namespace WebApi.Business.src.Services.Abstractions.ServiceAbractions
{
    public interface IUSerService
    {
        IEnumerable<UserDto> GetAllUsers(); 
        UserDto CreateUser(UserDto userDto);
        UserDto CreateUserByAdmin(UserDto userAdminDto);
        UserDto GetUserById(Guid id);
        UserUpdateDto UpdateUser(Guid id, UserUpdateDto userUpdateDto);
        UserDto UpdateUserByAdmin(Guid id, UserDto userAdminDto);
        UserDto DeleteUser (Guid id);
    }
}