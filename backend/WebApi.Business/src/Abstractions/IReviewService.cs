using WebApi.Business.src.Dto;
using WebApi.Domain.src.Entities;

namespace WebApi.Business.src.Abstractions
{
    public interface IReviewService
    {
        Task<Book> AddReview(ReviewDto review);
        Task<List<ReviewDto>> BookReviews(Guid BookId);
    }
}