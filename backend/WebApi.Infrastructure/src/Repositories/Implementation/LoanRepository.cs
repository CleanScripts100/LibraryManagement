using Microsoft.EntityFrameworkCore;
using WebApi.Business.src.Shared;
using WebApi.Domain.src.Abstractions;
using WebApi.Domain.src.Entities;
using WebApi.Infrastructure.src.Database;

namespace WebApi.Infrastructure.src.Repositories
{
    public class LoanRepository : ILoanRepository
    {
        private readonly DatabaseContext _dbContext;
        private readonly DbSet<Book> _books;
        private readonly DbSet<Loan> _loans;
        public LoanRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
            _books = _dbContext.Books;
            _loans = _dbContext.Loans;
        }
        public async Task<Loan> LoanBook(Guid UserId, List<Guid> BooksId)
        {
            //confirm if userId is an actual user 
            var user = await _dbContext.Users.FindAsync(UserId) ?? throw CustomException.NotFoundException("User Not Found");
            // get the books with the Guid
            var books = await _books.Where(b => BooksId.Contains(b.Id)).ToListAsync();
            //check the inventory count if it is more than one
            if (books.Any())
            {

                foreach (var book in books)
                {
                    if (book != null && book.InventoryCount > 0)
                    {
                        // deduct 1 from the inventory count
                        book.InventoryCount--;
                    }
                    else
                    {
                        throw new CustomException(400, "Inventory Count is lessthan or equal to zero");
                    }
                }

                //create a loan entity with the info 
                Loan loan = new()
                {
                    BookId = BooksId,
                    UserId = UserId,
                    ReturnDate = DateTime.UtcNow.AddDays(7),
                    Books = books,
                    Status = Domain.src.Enums.LoanStatus.Borrowed
                };

                //insert the loan entity
                await _loans.AddAsync(loan);

                //save changes
                await _dbContext.SaveChangesAsync();

                return loan;
            }
            throw CustomException.NotFoundException("No Book Found");
        }
     
        public async Task<bool> ReturnLoanedBooks(Guid Userid, Guid LoanId)
        {
            _ = await _dbContext.Users.FindAsync(Userid) ?? throw CustomException.NotFoundException("User Not Found");
            var loanedBook = await _loans.FindAsync(LoanId);

            if (loanedBook != null)
            {
                foreach (var book in loanedBook.BookId!)
                {
                    var actualBook = await _books.FindAsync(book);
                    actualBook!.InventoryCount += 1;
                    _books.Update(actualBook);
                }
                loanedBook.Status = Domain.src.Enums.LoanStatus.Returned;
                loanedBook.ReturnDate = DateTime.UtcNow;
                loanedBook.UpdatedAt = DateTime.UtcNow;
                _loans.Update(loanedBook);

                await _dbContext.SaveChangesAsync();
                return true;
            }
            throw CustomException.NotFoundException("Loan Not Found");

        }

        public async Task<IEnumerable<Loan>> GetAllLoanedBooks()
        {
            var loans = await _loans.Include(l => l.Books).ToListAsync();
            if (loans.Count == 0)
            {
                throw CustomException.NotFoundException("No Books Loans Yet");
            }

            foreach (var loan in loans)
            {
                loan.Books = await _books.Where(b => loan.BookId!.Contains(b.Id)).ToListAsync();
            }
            
            return loans;
        }

        public async Task<IEnumerable<Loan>> GetUserLoanedBooks(Guid UserId)
        {
            var loans = await _loans.Where(l => l.UserId == UserId).Include(l => l.Books).ToListAsync();
            if (loans.Count == 0)
            {
                throw CustomException.NotFoundException("You have No Loans Yet");
            }

            foreach (var loan in loans)
            {
                loan.Books = await _books.Where(b => loan.BookId!.Contains(b.Id)).ToListAsync();
            }

            return loans;
        }
    }
}