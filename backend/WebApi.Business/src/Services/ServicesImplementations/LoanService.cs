
using WebApi.Business.src.Abstractions;
using WebApi.Business.src.Dto;
using WebApi.Domain.src.Abstractions;
using WebApi.Domain.src.Entities;

namespace WebApi.Business.src.Services.ServicesImplementations
{
    public class LoanService : ILoanService
    {
        private readonly ILoanRepository _loanRepository;
        public LoanService(ILoanRepository loanRepo)
        {
            _loanRepository = loanRepo;
        }
        public async Task<IEnumerable<LoanViewDto>> GetAllLoanedBooks()
        {
            var loans = await _loanRepository.GetAllLoanedBooks();
            List<LoanViewDto> LoanViewDto = new List<LoanViewDto>();
            foreach (var loan in loans)
            {
                var dd = new LoanViewDto()
                {
                    Id = loan.Id,
                    ReturnDate = loan.ReturnDate,
                    Status = loan.Status.ToString(),
                    UserId = loan.UserId,
                    books = BookIdListToBooDtoList(loan.Books)
                };
                LoanViewDto.Add(dd);
            }
            return LoanViewDto;
        }

        public async Task<IEnumerable<LoanViewDto>> GetUserLoanedBooks(Guid UserId)
        {
            var loans = await _loanRepository.GetUserLoanedBooks(UserId);
            List<LoanViewDto> LoanViewDto = new List<LoanViewDto>();
            foreach (var loan in loans)
            {
                var dto = new LoanViewDto()
                {
                    Id = loan.Id,
                    ReturnDate = loan.ReturnDate,
                    Status = loan.Status.ToString(),
                    UserId = loan.UserId,
                    books = BookIdListToBooDtoList(loan.Books)
                };
                LoanViewDto.Add(dto);
            }
            return LoanViewDto;
        }
        public async Task<LoanViewDto> LoanBook(Guid UserId, List<Guid> BooksId)
        {
            var loan = await _loanRepository.LoanBook(UserId, BooksId);
            var output = new LoanViewDto()
            {
                Id = loan.Id,
                UserId = loan.UserId,
                ReturnDate = loan.ReturnDate,
                Status = loan.Status.ToString(),
                books = BookIdListToBooDtoList(loan.Books!)
            };
            return output;
        }

        private static List<BookDto> BookIdListToBooDtoList(List<Book> Books)
        {
            //convert the book to bookdto
            List<BookDto> bookDTOList = Books.Select(book => new BookDto
            {
                Title = book.Title,
                Author = book.Author,
                Description = book.Description,
                Genre = book.Genre.ToString(),
                Images = book.Images,
                ISBN = book.ISBN,
                PageCount = book.PageCount,
                PublishedYear = book.PublishedYear
            }
              ).ToList();
            //  return Bookdto
            return bookDTOList;
        }

        public async Task<bool> ReturnLoanedBooks(Guid UserId, Guid LoanId)
        {
            var result = await _loanRepository.ReturnLoanedBooks(UserId, LoanId);
            return result;
        }
    }
}