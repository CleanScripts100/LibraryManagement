
using AutoMapper;
using WebApi.Business.src.Abstractions;
using WebApi.Business.src.Dto;
using WebApi.Business.src.Shared;
using WebApi.Domain.src.Abstractions;
using WebApi.Domain.src.Entities;

namespace WebApi.Business.src.Services.ServicesImplementations
{
    public class ReviewService : IReviewService
    {
        private readonly IMapper _mapper;
        private readonly IReviewRepository _reviewRepository;
        public ReviewService(IMapper mapper, IReviewRepository reviewRepo)
        {
            _mapper = mapper;
            _reviewRepository = reviewRepo;
        }
        public async Task<Book> AddReview(ReviewDto dto)
        {
            var review = _mapper.Map<Review>(dto);
            var book = await _reviewRepository.AddReview(review);
            return book;
        }

        public async Task<List<ReviewDto>> BookReviews(Guid BookId)
        {
            var reviews = await _reviewRepository.BookReviews(BookId);
            return _mapper.Map<List<ReviewDto>>(reviews);

        }
    }
}