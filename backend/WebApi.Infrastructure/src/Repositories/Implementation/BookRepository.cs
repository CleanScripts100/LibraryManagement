using Microsoft.EntityFrameworkCore;
using WebApi.Business.src.Services.Abstractions.RepoAbstractions;
using WebApi.Business.src.Shared;
using WebApi.Domain.src.Entities;
using WebApi.Infrastructure.src.Database;

namespace WebApi.Infrastructure.src.Repositories.Implementation
{
    public class BookRepository : IBookRepository
    {
        private readonly DatabaseContext _dbContext;
        private readonly DbSet<Book> _books;
        private readonly DbSet<Loan> _loans;
        private readonly DbSet<Review> _reviews;

        public BookRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
            _books = _dbContext.Books;
            _loans = _dbContext.Loans;
            _reviews = _dbContext.Reviews;
        }

        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            return await _books.ToListAsync();
        }

        public async Task<Book> GetBookById(Guid bookId)
        {
            return await _books.FindAsync(bookId);
        }

        public async Task<Book> AddBook(Book book)
        {
            await _books.AddAsync(book);
            await _dbContext.SaveChangesAsync();
            return book;
        }

        public async Task<Book> UpdateBook(Guid bookId, Book book)
        {
            var updateBook = _books.Find(bookId);
            if (updateBook != null)
            {
                updateBook.Title = book.Title ?? book.Title;
                updateBook.Description = book.Description;
                updateBook.InventoryCount = book.InventoryCount;
                updateBook.PageCount = book.PageCount;
                updateBook.ISBN = book.ISBN!;
                updateBook.PublishedYear = book.PublishedYear!;
                updateBook.Images = book.Images!;
                updateBook.Author = book.Author!;   
                updateBook.Genre = book.Genre;  
                updateBook.UpdatedAt = DateTime.UtcNow;          
                
                await _dbContext.SaveChangesAsync();
                return updateBook;
            }
            return updateBook!;
        }

        public async Task<Book> DeleteBook(Guid bookId)
        {
            var deleteBook = _books.Find(bookId);
            if (deleteBook != null)
            {
                _books.Remove(deleteBook);
                await _dbContext.SaveChangesAsync();
                return deleteBook;
            }
            return deleteBook!;
        }

        public async Task<Book> AddReview(Review review)
        {
            await _reviews.AddAsync(review);
            await _dbContext.SaveChangesAsync();
            var book = await _books.FindAsync(review.BookId);
            return book!;

        }

        public async Task<Book> LoanBook(Guid UserId, List<Guid> BooksId)
        {
            var user = await _dbContext.Users.FindAsync(UserId);
            if (user == null)
            {
                throw CustomException.NotFoundException("User Not Found");
            }
            var books = await _books.Where(b => BooksId.Contains(b.Id)).ToListAsync();
            if (books.Any())
            {
                foreach (var book in books)
                {
                    if (book != null && book.InventoryCount > 0)
                    {
                        book.InventoryCount -= 1;
                        Loan loan = new Loan()
                        {
                            BookId = book.Id,
                            UserId = UserId,
                            ReturnDate = DateTime.UtcNow.AddDays(7),
                            Status = Domain.src.Enums.LoanStatus.Borrowed
                        };
                        await _loans.AddAsync(loan);
                        await _dbContext.SaveChangesAsync();
                        return book;
                    }
                    throw new CustomException(400, "Inventory Count is lessthan or equal to zero");
                }
            }

            throw CustomException.NotFoundException();
        }

        public async Task<Book> ReturnLoanedBooks(Guid Userid, Guid LoanId)
        {
            var user = await _dbContext.Users.FindAsync(Userid);
            if (user == null)
            {
                throw CustomException.NotFoundException("User Not Found");
            }
            var loanedBook = await _loans.FindAsync(LoanId);

            if (loanedBook != null)
            {
                var actualBook = await _books.FindAsync(loanedBook.BookId);
                actualBook!.InventoryCount += 1;
                loanedBook.Status = Domain.src.Enums.LoanStatus.Returned;
                _loans.Update(loanedBook);
                _books.Update(actualBook);
                await _dbContext.SaveChangesAsync();
                return actualBook;
            }
            throw CustomException.NotFoundException("Loan Not Found");
        }
    }
}