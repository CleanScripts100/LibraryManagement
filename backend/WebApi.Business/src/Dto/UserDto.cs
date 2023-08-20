using AutoMapper;
using System.ComponentModel.DataAnnotations;
using WebApi.Domain.src.Entities;
using WebApi.Domain.src.Enums;

namespace WebApi.Business.src.Dto;

[AutoMap(typeof(User))]
public class UserDto
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Image { get; set; }
    public string? Role { get; set; }
    
}

public class UserUpdateDto
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Image { get; set; }
    public Gender? Gender { get; set; }
    [EmailAddress]
    public string Email { get; set; } = null!;
}
public class UserCredentialsDto
{
    public string Email { get; set; } = null!; 
    public string Password { get; set; } = null!;
}

public class UserReadDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!; 
    public string Avatar { get; set; } = null!;
    public string? Role { get; set; }
}

public class UserCreateDto
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    [EmailAddress]
    public string Email { get; set; } = null!;
    public string? Gender { get; set; }
    public string Avatar { get; set; } = null!; 
    public string Password { get; set; } = null!;
}
