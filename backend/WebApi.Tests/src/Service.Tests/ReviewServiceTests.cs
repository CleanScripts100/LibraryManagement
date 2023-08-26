using AutoMapper;
using Moq;
using WebApi.Business.src.Dto;
using WebApi.Business.src.Services.ServicesImplementations;
using WebApi.Domain.src.Abstractions;
using WebApi.Domain.src.Entities;
using WebApi.Infrastructure.src.Configuration;

namespace WebApi.Tests
{
    public class ReviewServiceTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IReviewRepository> _reviewRepositoryMock;

        public ReviewServiceTests()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapperProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

            _reviewRepositoryMock = new Mock<IReviewRepository>();
        }

        [Fact]
        public async Task AddReview_ValidDto_ReturnsBook()
        {
            // Arrange
            var dto = new ReviewDto
            {
                BookId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Rating = 5,
                Comment = "Great book!"
            };
            var review = _mapper.Map<Review>(dto);
            var book = new Book
            {
                Author = new List<string> { "Adam James" },
                Images = new List<string> { "1.jpg" },
            };

            _reviewRepositoryMock.Setup(repo => repo.AddReview(It.IsAny<Review>())).ReturnsAsync(book);

            var reviewService = new ReviewService(_mapper, _reviewRepositoryMock.Object);

            // Act
            var result = await reviewService.AddReview(dto);

            // Debugging: Print the result and dto to the console
            Console.WriteLine("Result: " + result); // Check if result is null
            Console.WriteLine("Dto: " + dto); // Check if dto is correctly initialized

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Book>(result);

        }

        [Fact]
        public async Task BookReviews_ValidId_ReturnsListOfReviewDtos()
        {
            // Arrange
            var bookId = Guid.NewGuid();
            var review1 = new Review
            {
                BookId = bookId,
                UserId = Guid.NewGuid(),
                Rating = 4,
                Comment = "Great book!"
            };
    
            var review2 = new Review
            {
                BookId = bookId,
                UserId = Guid.NewGuid(),
                Rating = 5,
                Comment = "Amazing read!"
            };

            var reviews = new List<Review> 
            {
                review1,
                review2
            };
            
            _reviewRepositoryMock.Setup(repo => repo.BookReviews(bookId)).ReturnsAsync(reviews);

            var reviewService = new ReviewService(_mapper, _reviewRepositoryMock.Object);

            // Act
            var result = await reviewService.BookReviews(bookId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<ReviewDto>>(result);
            Assert.Equal(reviews.Count, result.Count);
        }
    }
}
