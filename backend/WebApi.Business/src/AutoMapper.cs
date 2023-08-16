using AutoMapper;
using WebApi.Business.src.Dto;
using WebApi.Domain.src.Entities;

namespace WebApi.Business.src
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
            {
                CreateMap<Book, BookDto>().ReverseMap();
            }
        }
}