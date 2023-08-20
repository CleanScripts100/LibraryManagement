using AutoMapper;
using WebApi.Business.src.Abstractions;
using WebApi.Business.src.Dto;
using WebApi.Business.src.Implementations;
using WebApi.Business.src.Shared;
using WebApi.Domain.src.Abstractions;
using WebApi.Domain.src.Entities;

namespace WebApiDemo.Business.src.Implementations
{
    public class UserService : BaseService<User, UserReadDto, UserCreateDto, UserUpdateDto>, IUserService 
    {
        private readonly IUserRepo _userRepo;
        public UserService(IUserRepo userRepo, IMapper mapper) : base(userRepo, mapper)
        {
            _userRepo = userRepo;
        }

        public async Task<UserReadDto> UpdatePassword(Guid id, string newPassword)
        {
            var foundUser = await _userRepo.GetOneById(id) ?? throw new Exception("User not found");
            PasswordService.HashPassword(newPassword, out var hashedPassword, out var salt);
            foundUser.Password = hashedPassword;
            foundUser.Salt = salt;
            foundUser.UpdatedAt = DateTime.UtcNow;
            return _mapper.Map<UserReadDto>(await _userRepo.UpdatePassword(foundUser));
        }

        public override async Task<UserReadDto> CreateOne(UserCreateDto dto)
        {
            var entity = _mapper.Map<User>(dto);
            PasswordService.HashPassword(dto.Password, out var hashedPassword, out var salt);
            entity.Password = hashedPassword;
            entity.Salt = salt;
            var created = await _userRepo.CreateOne(entity);
            return _mapper.Map<UserReadDto>(created);
        }

        public override async Task<UserReadDto> UpdateOneById(Guid id, UserUpdateDto updated)
        {
            var foundItem = await base.GetEntityById(id) ?? throw CustomException.NotFoundException();
            foundItem.firstName = updated.FirstName!;
            foundItem.lastName = updated.LastName!;
            foundItem.Email = updated.Email;
            foundItem.Gender = (WebApi.Domain.src.Enums.Gender)updated.Gender;
            foundItem.Image = updated.Image!;
            foundItem.UpdatedAt = DateTime.UtcNow;

            var updatedEntity = await _userRepo.UpdateOneById(foundItem);
            return _mapper.Map<UserReadDto>(updatedEntity);

        }
        
        public async Task<UserReadDto> CreateAdmin(UserCreateDto dto)
        {
            var entity = _mapper.Map<User>(dto);
            PasswordService.HashPassword(dto.Password, out var hashedPassword, out var salt);
            entity.Password = hashedPassword;
            entity.Salt = salt;
            var created = await _userRepo.CreateAdmin(entity);
            return _mapper.Map<UserReadDto>(created);
        }
    }
}