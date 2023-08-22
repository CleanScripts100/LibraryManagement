
// using AutoMapper;
// using Moq;
// using WebApi.Business.src.Dto;
// using WebApi.Business.src.Services.ServicesImplementations;
// using WebApi.Domain.src.Abstractions;
// using WebApi.Domain.src.Entities;
// using WebApi.Infrastructure.src.Configuration;

// namespace WebApi.Tests.src.Service.Tests
// {
//     public class ReviewServiceTests
//     {
//         [Fact]
//         public async Task AddReview_ValidInput_ReturnsBook()
//         {
//             // Arrange
//             var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile<MapperProfile>());
//             IMapper mapper = mapperConfig.CreateMapper();

//             var mockRepository = new Mock<IReviewRepository>();
//             var reviewService = new ReviewService(mapper, mockRepository.Object);

//             var reviewDto = new ReviewDto { Rating = 5, Comment = "Great book" };
//             var reviewEntity = new Review { Rating = reviewDto.Rating, Comment = reviewDto.Comment };
//             mockRepository.Setup(repo => repo.AddReview(It.IsAny<Review>()))
//                 .ReturnsAsync(new Book
//                 {
//                     Id = Guid.NewGuid(),
//                     Title = "Sample Title",
//                     Author = {"Sample Author"}, // Set required Author property
//                     Images = new List<string> {"sample.jpg"}     // Set required Images property
//                     // Set other properties as needed
//                 });

//             // Act
//             var result = await reviewService.AddReview(reviewDto);

//             // Assert
//             Assert.NotNull(result);
//             Assert.IsType<Book>(result);
//         }

//         [Fact]
//         public async Task BookReviews_ValidBookId_ReturnsListOfReviewDto()
//         {
//             // Arrange
//             var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile<MapperProfile>());
//             IMapper mapper = mapperConfig.CreateMapper();

//             var mockRepository = new Mock<IReviewRepository>();
//             var bookId = Guid.NewGuid();
//             var reviews = new List<Review>
//             {
//                 new Review { Rating = 5, Comment = "Excellent" },
//                 new Review { Rating = 4, Comment = "Good" },
//             };
//             mockRepository.Setup(repo => repo.BookReviews(bookId))
//                 .ReturnsAsync(reviews);

//             var reviewService = new ReviewService(mapper, mockRepository.Object);

//             // Act
//             var result = await reviewService.BookReviews(bookId);

//             // Assert
//             Assert.NotNull(result);
//             Assert.Equal(reviews.Count, result.Count);
//         }
//     }
// }