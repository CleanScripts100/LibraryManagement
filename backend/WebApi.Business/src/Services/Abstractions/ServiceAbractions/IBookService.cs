using WebApi.Business.src.Dto;

namespace WebApi.Business.src.Services.Abstractions.ServiceAbractions
{
    public interface IBookService
    {
        BookDto AddBook(BookDto book);
        BookDto GetBookById(Guid id);
        BookDto UpdateBook(Guid id, BookDto book);
        BookDto DeleteBook(Guid id);
        IEnumerable<BookDto> GetAllBooks();
    }
}