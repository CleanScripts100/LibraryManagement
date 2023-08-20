
using Microsoft.EntityFrameworkCore;
using WebApi.Business.src.Shared;
using WebApi.Domain.src.Entities;
using WebApi.Infrastructure.src.Database;

namespace WebApi.Infrastructure.src.Repositories
{
    public class ReviewRepository
    {
        private readonly DatabaseContext _dbContext;
        private readonly DbSet<Book> _books;
        private readonly DbSet<Review> _reviews;
        public ReviewRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
            _books = _dbContext.Books;
            _reviews = _dbContext.Reviews;
        }

        public async Task<Book> AddReview(Review review)
        {
            var user = await _dbContext.Users.FindAsync(review.UserId) ?? throw CustomException.NotFoundException("User not found");
            var query = _reviews.Where(r => r.BookId == review.BookId && r.UserId == review.UserId);
            if (query.Any())
            {
                throw new CustomException(400, "You can not review the same book twice");
            }
            await _reviews.AddAsync(review);
            await _dbContext.SaveChangesAsync();
            var book = await _books.FindAsync(review.BookId);
            return book!;

        }

        public async Task<List<Review>> BookReviews(Guid BookId)
        {
            var reviews = await _reviews.Where(r => r.BookId == BookId).ToListAsync();
            return reviews;
        }
    }
}