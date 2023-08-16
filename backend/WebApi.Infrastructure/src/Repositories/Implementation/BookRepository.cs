using Microsoft.EntityFrameworkCore;
using WebApi.Business.src.Services.Abstractions.RepoAbstractions;
using WebApi.Domain.src.Entities;
using WebApi.Infrastructure.src.Database;

namespace WebApi.Infrastructure.src.Repositories.Implementation
{
    public class BookRepository : IBookRepository
    {
        private readonly DatabaseContext _dbContext;
        private readonly DbSet<Book> _books;

        public BookRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
            _books = _dbContext.Books;
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _books.ToList();
        }

        public Book GetBookById(Guid bookId)
        {
            return _books.Find(bookId);
        }

        public Book AddBook(Book book)
        {
            _books.Add(book);
            _dbContext.SaveChanges();
            return book;
        }

        public Book UpdateBook(Guid bookId, Book book)
        {
            var updateBook = _books.Find(bookId);
            if (updateBook != null)
            {
                updateBook.Title = book.Title ?? book.Title;
                updateBook.Description = book.Description;
                updateBook.InventoryCount = book.InventoryCount;
                updateBook.Images = book.Images;                
                Console.WriteLine("Book Updated");
                _dbContext.SaveChanges();
                return updateBook;
            }
            return updateBook;
        }

        public Book DeleteBook(Guid bookId)
        {
            var deleteBook = _books.Find(bookId);
            if (deleteBook != null)
            {
                _books.Remove(deleteBook);
                _dbContext.SaveChanges();
                Console.WriteLine("Book Deleted");
                return deleteBook;
            }
            return deleteBook;
        }
    }
}