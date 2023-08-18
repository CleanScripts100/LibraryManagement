using AutoMapper;
using WebApi.Business.src.Dto;
using WebApi.Domain.src.Entities;

namespace WebApi.Infrastructure.src.Configuration
{
    public class MapperProfile : Profile
    {
      public MapperProfile()
      {
        CreateMap<Book, BookDto>().ReverseMap();
        CreateMap<User, UserReadDto>()
          .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => $"{src.firstName}"))
          .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => $"{src.firstName}"))
          .ForMember(dest => dest.Avatar, opt => opt.MapFrom(src => $"{src.Image}"))
          .ReverseMap();
        CreateMap<UserUpdateDto, User>()
          .ForMember(dest => dest.firstName, opt => opt.MapFrom(src => $"{src.firstName}"))
          .ForMember(dest => dest.lastName, opt => opt.MapFrom(src => $"{src.lastName}"))
          .ForMember(dest => dest.Image, opt => opt.MapFrom(src => $"{src.image}"))
          .ReverseMap();
        CreateMap<UserCreateDto, User>()
          .ForMember(dest => dest.firstName, opt => opt.MapFrom(src => $"{src.FirstName}"))
          .ForMember(dest => dest.lastName, opt => opt.MapFrom(src => $"{src.LastName}"))
          .ForMember(dest => dest.Image, opt => opt.MapFrom(src => $"{src.Avatar}"))
          .ReverseMap();
        CreateMap<ReviewDto, Review>().ReverseMap();
      }
    }
}