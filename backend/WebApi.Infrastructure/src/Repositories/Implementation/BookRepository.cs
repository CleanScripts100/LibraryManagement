using Microsoft.EntityFrameworkCore;
using WebApi.Domain.src.Abstractions;
using WebApi.Domain.src.Entities;
using WebApi.Domain.src.Shared;
using WebApi.Infrastructure.src.Database;
using static WebApi.Domain.src.Shared.QueryOptions;

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

        public async Task<IEnumerable<Book>> GetAllBooks(QueryOptions queryOptions)
        {

            IQueryable<Book> query = _books;
            List<Book> books = new();
            if (queryOptions.Search != null)
            {

                query = queryOptions.Search.SearchKey switch
                {
                    SearchKey.Author => _books.Where(b => b.Author.Contains(queryOptions.Search.SearchKeyValue.ToLower())),
                    SearchKey.Title => _books.Where(b => b.Title == queryOptions.Search.SearchKeyValue.ToLower()),
                    SearchKey.Genre => _books.Where(b => b.Genre.ToString() == queryOptions.Search.SearchKeyValue.ToLower()),
                    _ => _books.Where(b => b.Title == queryOptions.Search.SearchKeyValue.ToLower()),
                };
            }

            if (queryOptions.PageNumber != null)
            {
                query = query.Skip(queryOptions.PageNumber.Value);
            }

            if (queryOptions.PerPage != null)
            {
                query = query.Take(queryOptions.PerPage.Value);
            }

            if (queryOptions.OrderByDesc)
            {
                query = query.OrderByDescending(b => b.Id);
            }

            books = await query.ToListAsync();
            return books;
        }

    }
}