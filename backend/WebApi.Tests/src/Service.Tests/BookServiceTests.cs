// using Moq;
// using AutoMapper;
// using WebApi.Business.src.Services.ServicesImplementations;
// using WebApi.Domain.src.Entities;
// using WebApi.Business.src.Services.Abstractions.RepoAbstractions;
// using WebApi.Business.src.Dto;

// namespace WebApi.Tests.src.Service.Tests
// {
//     public class BookServiceTests
//     {
//         [Theory]
//         [InlineData("Existing Book", "John Doe", ["image.jpg"], "Updated Book", "Jane Smith", "newimage.jpg")]
//         // Add more test cases as needed
//         public async Task UpdateBook_ValidId_ReturnsUpdatedBookDto(
//             string existingTitle, string existingAuthor, string existingImages,
//             string updatedTitle, string updatedAuthor, string updatedImages)
//         {
//             // Arrange
//             var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile<YourAutoMapperProfile>());
//             IMapper mapper = mapperConfig.CreateMapper();

//             var mockRepository = new Mock<IBookRepository>();
//             var existingBook = new Book
//             {
//                 Id = Guid.NewGuid(),
//                 Title = existingTitle,
//                 Author = existingAuthor,
//                 Images = existingImages,
//                 // Set other properties as needed
//             };
//             mockRepository.Setup(repo => repo.GetBookById(existingBook.Id)).ReturnsAsync(existingBook);
//             mockRepository.Setup(repo => repo.UpdateBook(existingBook.Id, It.IsAny<Book>()))
//                 .ReturnsAsync(existingBook);

//             var bookService = new BookService(mapper, mockRepository.Object);

//             // Act
//             var updatedBook = new Book
//             {
//                 Id = existingBook.Id,
//                 Title = updatedTitle,
//                 Author = updatedAuthor,
//                 Images = updatedImages,
//                 // Set other properties as needed
//             };
//             var result = await bookService.UpdateBook(existingBook.Id, updatedBook);

//             // Assert
//             Assert.NotNull(result);
//             Assert.Equal(updatedBook.Id, result.Id);
//             Assert.Equal(updatedBook.Title, result.Title);
//             // Add more assertions as needed
//         }

//         [Fact]
//         public async Task AddBook_ValidInput_ReturnsAddedBookDto()
//         {
//             // Arrange
//             var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile<YourAutoMapperProfile>());
//             IMapper mapper = mapperConfig.CreateMapper();

//             var mockRepository = new Mock<IBookRepository>();
//             var bookService = new BookService(mapper, mockRepository.Object);

//             var newBookDto = new BookDto { Title = "New Book", ISBN = "1234567890" };

//             mockRepository.Setup(repo => repo.AddBook(It.IsAny<Book>()))
//                 .ReturnsAsync(new Book { Id = Guid.NewGuid(), Title = newBookDto.Title });

//             // Act
//             var result = await bookService.AddBook(newBookDto);

//             // Assert
//             Assert.NotNull(result);
//             Assert.Equal(newBookDto.Title, result.Title);
//         }

//         [Fact]
//         public async Task DeleteBook_ValidId_ReturnsDeletedBookDto()
//         {
//             // Arrange
//             var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>());
//             IMapper mapper = mapperConfig.CreateMapper();

//             var mockRepository = new Mock<IBookRepository>();
//             var existingBookId = Guid.NewGuid();
//             mockRepository.Setup(repo => repo.DeleteBook(existingBookId))
//                 .ReturnsAsync(new Book { Id = existingBookId, Title = "Deleted Book" });

//             var bookService = new BookService(mapper, mockRepository.Object);

//             // Act
//             var result = await bookService.DeleteBook(existingBookId);

//             // Assert
//             Assert.NotNull(result);
//             Assert.Equal(existingBookId, result.Id);
//             // Add more assertions as needed
//         }

//         [Fact]
//         public async Task GetAllBooks_ReturnsListOfBookDto()
//         {
//             // Arrange
//             var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile<YourAutoMapperProfile>());
//             IMapper mapper = mapperConfig.CreateMapper();

//             var mockRepository = new Mock<IBookRepository>();
//             var existingBooks = new List<Book>
//             {
//                 new Book { Id = Guid.NewGuid(), Title = "Book 1" },
//                 new Book { Id = Guid.NewGuid(), Title = "Book 2" },
//                 // Add more books as needed
//             };
//             mockRepository.Setup(repo => repo.GetAllBooks())
//                 .ReturnsAsync(existingBooks);

//             var bookService = new BookService(mapper, mockRepository.Object);

//             // Act
//             var result = await bookService.GetAllBooks();

//             // Assert
//             Assert.NotNull(result);
//             Assert.Equal(existingBooks.Count, result.Count());
//         }
//     }
// }