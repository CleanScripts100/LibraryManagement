using AutoMapper;
using WebApi.Business.src.Dto;
using WebApi.Domain.src.Entities;

namespace WebApi.Infrastructure.src.Configuration
{
    public class MapperProfile : Profile
    {
      public MapperProfile()
      {
        CreateMap<Book, BookDto>()
          .ForMember(destinationMember: dest => dest.Genre, memberOptions: opt => opt.MapFrom(src => $"{src.Genre}"));
        CreateMap<Book, BookDto>().ReverseMap();
        CreateMap<User, UserReadDto>()
          .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => $"{src.firstName}"))
          .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => $"{src.firstName}"))
          .ForMember(dest => dest.Avatar, opt => opt.MapFrom(src => $"{src.Image}"))
          .ForMember(dest => dest.Role, opt => opt.MapFrom(src => $"{src.Role}"))
          .ReverseMap();
        CreateMap<UserUpdateDto, User>()
          .ForMember(dest => dest.firstName, opt => opt.MapFrom(src => $"{src.FirstName}"))
          .ForMember(dest => dest.lastName, opt => opt.MapFrom(src => $"{src.LastName}"))
          .ForMember(dest => dest.Image, opt => opt.MapFrom(src => $"{src.Image}"))
          .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => $"{src.Gender}"))
          .ReverseMap();
        CreateMap<UserCreateDto, User>()
          .ForMember(dest => dest.firstName, opt => opt.MapFrom(src => $"{src.FirstName}"))
          .ForMember(dest => dest.lastName, opt => opt.MapFrom(src => $"{src.LastName}"))
          .ForMember(dest => dest.Image, opt => opt.MapFrom(src => $"{src.Avatar}"))
          .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => $"{src.Gender}"))
          .ReverseMap();
        CreateMap<ReviewDto, Review>().ReverseMap();
      }
    }
}