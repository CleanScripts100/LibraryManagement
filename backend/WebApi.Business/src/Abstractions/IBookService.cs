using WebApi.Business.src.Dto;
using WebApi.Domain.src.Entities;

namespace WebApi.Business.src.Services.Abstractions.ServiceAbractions
{
    public interface IBookService
    {
      Task<BookDto> AddBook(BookDto book);
      Task<BookDto> GetBookById(Guid id);
      Task<BookDto> UpdateBook(Guid id, BookDto book);
      Task<BookDto> DeleteBook(Guid id);
      Task<IEnumerable<BookDto>> GetAllBooks();
      Task<BookDto> LoanBook(Guid UserId, List<Guid> BooksId);
      Task<bool> ReturnLoanedBooks(Guid UserId, Guid LoanId);
      Task<IEnumerable<Loan>> GetAllLoanedBooks();
      Task<IEnumerable<Loan>> GetUserLoanedBooks(Guid UserId);
      Task<Book> AddReview(ReviewDto review);
    }
}