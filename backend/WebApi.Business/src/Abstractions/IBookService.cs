using WebApi.Business.src.Dto;

namespace WebApi.Business.src.Services.Abstractions.ServiceAbractions
{
    public interface IBookService
    {
      Task<BookDto> AddBook(BookDto book);
      Task<BookDto> GetBookById(Guid id);
      Task<BookDto> UpdateBook(Guid id, BookDto book);
      Task<BookDto> DeleteBook(Guid id);
      Task<IEnumerable<BookReadDto>> GetAllBooks();
    }
}