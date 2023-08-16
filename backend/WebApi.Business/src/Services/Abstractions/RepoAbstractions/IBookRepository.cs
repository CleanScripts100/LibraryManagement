using WebApi.Domain.src.Entities;

namespace WebApi.Business.src.Services.Abstractions.RepoAbstractions
{
    public interface IBookRepository
    {
        IEnumerable<Book> GetAllBooks();
        Book GetBookById(Guid Id);
        Book UpdateBook(Guid Id, Book book);
        Book DeleteBook(Guid Id);
        Book AddBook(Book book);
    }
}