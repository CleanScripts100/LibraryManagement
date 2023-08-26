using AutoMapper;
using Moq;
using WebApi.Business.src.Dto;
using WebApi.Domain.src.Abstractions;
using WebApi.Domain.src.Entities;
using WebApi.Domain.src.Enums;
using WebApi.Infrastructure.src.Configuration;
using WebApiDemo.Business.src.Implementations;

namespace WebApi.Tests.src.Service.Tests
{
    public class UserServiceTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUserRepo> _userRepoMock;

        public UserServiceTests()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapperProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

            _userRepoMock = new Mock<IUserRepo>();
        }

        [Fact]
        public async Task CreateOne_ValidDto_ReturnsUserReadDto()
        {
            // Arrange
            var dto = new UserCreateDto
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john@example.com",
                Gender = "Male",
                Avatar = "avatar.jpg",
                Password = "password"            
            };

            var user = _mapper.Map<User>(dto);
            _userRepoMock.Setup(repo => repo.CreateOne(It.IsAny<User>())).ReturnsAsync(user);

            var userService = new UserService(_userRepoMock.Object, _mapper);

            // Act
            var result = await userService.CreateOne(dto);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<UserReadDto>(result);
            Assert.Equal(user.Id, result.Id);
            Assert.Equal(user.FirstName, result.FirstName);
            Assert.Equal(user.LastName, result.LastName);
            Assert.Equal(user.Email, result.Email);
            Assert.Equal(user.Image, result.Avatar);
        }

        [Fact]
        public async Task UpdateOneById_ValidIdAndDto_ReturnsUserReadDto()
        {
            // Arrange
            var id = Guid.NewGuid();
            var updatedDto = new UserUpdateDto
            {
                FirstName = "Jane",
                LastName = "Doe",
                Image = "new-avatar.jpg",
                Gender = Gender.Female,
                Email = "jane@example.com"
            };
            var user = new User
            {
                FirstName = "David",
                LastName = "Greg",
                Image = "avatar.jpg",
                Gender = Gender.Male,
                Email = "david@example.com"
            };
            _userRepoMock.Setup(repo => repo.GetOneById(id)).ReturnsAsync(user);
            _userRepoMock.Setup(repo => repo.UpdateOneById(It.IsAny<User>())).ReturnsAsync(user);

            var userService = new UserService(_userRepoMock.Object, _mapper);

            // Act
            var result = await userService.UpdateOneById(id, updatedDto);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<UserReadDto>(result);
            Assert.Equal(user.Id, result.Id);
            Assert.Equal(user.FirstName, result.FirstName);
            Assert.Equal(user.LastName, result.LastName);
            Assert.Equal(user.Email, result.Email);
            Assert.Equal(user.Image, result.Avatar);
        }

        [Fact]
        public async Task UpdatePassword_ValidId_ReturnsUpdatedUser()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var newPassword = "newpassword";
            var foundUser = new User
            {
                Id = userId,
                FirstName = "John",
                LastName = "Doe",
                Email = "john@example.com",
                Password = null,
                Salt = null
            };
            _userRepoMock.Setup(repo => repo.GetOneById(userId)).ReturnsAsync(foundUser);
            _userRepoMock.Setup(repo => repo.UpdatePassword(It.IsAny<User>())).ReturnsAsync(foundUser);

            var userService = new UserService(_userRepoMock.Object, _mapper);

            // Act
            var result = await userService.UpdatePassword(userId, newPassword);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(userId, result.Id);
            Assert.Equal(foundUser.FirstName, result.FirstName);
            Assert.Equal(foundUser.LastName, result.LastName);
            Assert.Equal(foundUser.Email, result.Email);

            // Assert that the password and salt are updated
            _userRepoMock.Verify(repo => repo.UpdatePassword(It.Is<User>(user =>
                user.Password != null && user.Salt != null
            )), Times.Once);
        }

        [Fact]
        public async Task CreateAdmin_ValidDto_ReturnsUserReadDto()
        {
            // Arrange
            var adminDto = new UserCreateDto
            {
                FirstName = "Admin",
                LastName = "User",
                Email = "admin@example.com",
                Gender = "Male",
                Avatar = "avatar.png",
                Password = "password"
            };

            var createdAdmin = new User
            {
                Id = Guid.NewGuid(),
                FirstName = adminDto.FirstName,
                LastName = adminDto.LastName,
                Email = adminDto.Email,
                Role = Role.Admin, 
                Password = "hashedPassword",
                Salt = null
            };

            _userRepoMock.Setup(repo => repo.CreateAdmin(It.IsAny<User>())).ReturnsAsync(createdAdmin);

            var userService = new UserService(_userRepoMock.Object, _mapper);

            // Act
            var result = await userService.CreateAdmin(adminDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(createdAdmin.Id, result.Id);
            Assert.Equal(createdAdmin.FirstName, result.FirstName);
            Assert.Equal(createdAdmin.LastName, result.LastName);
            Assert.Equal(createdAdmin.Email, result.Email);
            Assert.Equal("Admin", result.Role); // Check if the role is "admin"
        }

    }
}