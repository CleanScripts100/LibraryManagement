using WebApi.Business.src.Dto;
namespace WebApi.Business.src.Abstractions
{
    public interface ILoanService
    {
        Task<LoanViewDto> LoanBook(Guid UserId, List<Guid> BooksId);
        Task<bool> ReturnLoanedBooks(Guid UserId, Guid LoanId);
        Task <IEnumerable<LoanViewDto>> GetAllLoanedBooks();
        Task <IEnumerable<LoanViewDto>> GetUserLoanedBooks(Guid userId);
    }
}