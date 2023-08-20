using WebApi.Domain.src.Entities;
using WebApi.Domain.src.Shared;

namespace WebApi.Domain.src.Abstractions
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllBooks();
        Task<IEnumerable<Book>> GetAllBooks(QueryOptions queryOptions);
        Task<Book> GetBookById(Guid Id);
        Task<Book> UpdateBook(Guid Id, Book book);
        Task<Book> DeleteBook(Guid Id);
        Task<Book> AddBook(Book book);
    }
}