
using WebApi.Domain.src.Entities;

namespace WebApi.Domain.src.Abstractions
{
    public interface ILoanRepository
    {
        Task<Loan> LoanBook(Guid UserId, List<Guid> BooksId);
        Task<bool> ReturnLoanedBooks(Guid UserId, Guid LoanId);
        Task<IEnumerable<Loan>> GetAllLoanedBooks();
        Task<IEnumerable<Loan>> GetUserLoanedBooks(Guid UserId);
    }
}