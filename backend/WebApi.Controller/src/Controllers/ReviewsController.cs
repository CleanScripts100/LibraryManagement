
using Microsoft.AspNetCore.Mvc;
using WebApi.Business.src.Abstractions;
using WebApi.Business.src.Dto;
using WebApi.Domain.src.Entities;

namespace WebApi.Controller.src.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ReviewsController
    {
        private readonly IReviewService _reviewService;
        public ReviewsController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpPost]
        public async Task<ActionResult<Book>> AddReview(ReviewDto review)
        {

            return await _reviewService.AddReview(review);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReviewDto>>> BookReviews(Guid bookId)
        {
            var result = await _reviewService.BookReviews(bookId);
            return result;

        }
    }
}