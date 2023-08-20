using WebApi.Domain.src.Entities;

namespace WebApi.Domain.src.Abstractions
{
    public interface IReviewRepository
    {
        Task<Book> AddReview(Review review);
        Task<List<Review>> BookReviews(Guid BookId);
    }
}