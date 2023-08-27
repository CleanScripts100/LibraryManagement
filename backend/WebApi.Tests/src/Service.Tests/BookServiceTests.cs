
using AutoMapper;
using Moq;
using WebApi.Business.src.Dto;
using WebApi.Business.src.Services.Abstractions.ServiceAbractions;
using WebApi.Business.src.Services.ServicesImplementations;
using WebApi.Domain.src.Abstractions;
using WebApi.Domain.src.Entities;
using WebApi.Domain.src.Enums;
using WebApi.Infrastructure.src.Configuration;

namespace WebApi.Tests.src.Service.Tests
{
    public class BookServiceTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IBookRepository> _bookRepositoryMock;

        public BookServiceTests()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapperProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

            _bookRepositoryMock = new Mock<IBookRepository>();
        }

        [Fact]
        public async Task AddBook_ValidDto_ReturnsBookDto()
        {
            // Arrange
            var dto = new BookDto
            {
                Author = new List<string> { "Author 1", "Author 2" },
                Images = new List<string> { "image1.jpg", "image2.jpg" },
                Title = "Sample Title",
                ISBN = "978-1234567890",
                PublishedYear = "2023",
                Description = "Sample description",
                PageCount = 200,
                InventoryCount = 50,
                Genre = "Fiction"
            };

            var book = _mapper.Map<Book>(dto);
            _bookRepositoryMock.Setup(repo => repo.AddBook(It.IsAny<Book>())).ReturnsAsync(book); // Make sure to set up the mock correctly

            var bookService = new BookService(_mapper, _bookRepositoryMock.Object);

            // Act
            var result = await bookService.AddBook(dto);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BookDto>(result);
            Assert.Equal(dto.Title, result.Title);
            Assert.Equal(dto.Description, result.Description);
            Assert.Equal(dto.Genre, result.Genre);
            Assert.Equal(dto.Author, result.Author);
            Assert.Equal(dto.ISBN, result.ISBN);
        }

        [Fact]
        public async Task GetAllBooks_ReturnsListOfBookReadDto()
        {
            // Arrange
            var books = new List<Book> 
            { 
                new Book
                {
                    Id = Guid.NewGuid(),
                    Author = new List<string> { "Author 1", "Author 2" },
                    Images = new List<string> { "image1.jpg", "image2.jpg" },
                    Title = "Sample Title",
                    ISBN = "978-1234567890",
                    PublishedYear = "2023",
                    Description = "Sample description",
                    PageCount = 200,
                    InventoryCount = 50,
                    Genre = Genre.Fiction
                },
                new Book
                {
                    Id = Guid.NewGuid(),
                    Author = new List<string> { "Author 3", "Author 4" },
                    Images = new List<string> { "image3.jpg", "image4.jpg" },
                    Title = "Second Sample Title",
                    ISBN = "978-1234567890",
                    PublishedYear = "2012",
                    Description = "Second Sample description",
                    PageCount = 100,
                    InventoryCount = 10,
                    Genre = Genre.Fiction
                },
            };

           _bookRepositoryMock.Setup(repo => repo.GetAllBooks()).ReturnsAsync(books);

            var bookService = new BookService(_mapper, _bookRepositoryMock.Object);

            // Act
            var result = await bookService.GetAllBooks();

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<BookReadDto>>(result);
            Assert.Equal(books.Count, result.Count());
        }

         [Fact]
        public async Task GetBookById_ValidId_ReturnsBookDto()
        {
            // Arrange
            var bookId = Guid.NewGuid();
            var foundBook = new Book
            {
                Author = new List<string> { "Author 3", "Author 4" },
                Images = new List<string> { "image3.jpg", "image4.jpg" },
                Title = "Second Sample Title",
                ISBN = "978-1234567890",
                PublishedYear = "2012",
                Description = "Second Sample description",
                PageCount = 100,
                InventoryCount = 10,
                Genre = Genre.Fiction
            };

            _bookRepositoryMock.Setup(repo => repo.GetBookById(bookId)).ReturnsAsync(foundBook);

            var bookService = new BookService(_mapper, _bookRepositoryMock.Object);

            // Act
            var result = await bookService.GetBookById(bookId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BookDto>(result);
            Assert.Equal(foundBook.Title, result.Title);
            Assert.Equal(foundBook.Author, result.Author);
        }

        [Fact]
        public async Task DeleteBook_ValidId_ReturnsDeletedBookDto()
        {
            // Arrange
            var bookId = Guid.NewGuid();
            var deletedBook = new Book
            {
                Author = new List<string> { "Author 3", "Author 4" },
                Images = new List<string> { "image3.jpg", "image4.jpg" },
                Title = "Second Sample Title",
                ISBN = "978-1234567890",
                PublishedYear = "2012",
                Description = "Second Sample description",
                PageCount = 100,
                InventoryCount = 10,
                Genre = Genre.Fiction
            };

            _bookRepositoryMock.Setup(repo => repo.DeleteBook(bookId)).ReturnsAsync(deletedBook);

            var bookService = new BookService(_mapper, _bookRepositoryMock.Object);

            // Act
            var result = await bookService.DeleteBook(bookId);
            var notFoundBook = await bookService.GetBookById(bookId);

            // Assert
            Assert.NotNull(result);
            Assert.Null(notFoundBook);
            Assert.IsType<BookDto>(result);
        }

        // [Fact]
        // public async Task UpdateBook_ValidDto_ReturnsUpdatedBookDto()
        // {
        //     // Arrange
        //     var bookId = Guid.NewGuid();
        //     var bookDto = new BookDto
        //     {
        //         Title = "Sample Title",
        //         Author = new List<string> { "Author 1", "Author 2" },
        //         Images = new List<string> { "image1.jpg", "image2.jpg" },
        //         ISBN = "978-1234567890",
        //         PublishedYear = "2012",
        //         Description = "Sample description",
        //         PageCount = 100,
        //         InventoryCount = 10,
        //         Genre = "Fiction"
        //     };

        //     var updatedBook = new Book
        //     {
        //         Title = "Changed Title",
        //         Author = new List<string> { "Changed Author 1"},
        //         Images = new List<string> { "image1.jpg", "image2.jpg" },
        //         ISBN = "99343-1234567890",
        //         PublishedYear = "2022",
        //         Description = "Sample description",
        //         PageCount = 50,
        //         InventoryCount = 2,
        //         Genre = Genre.Novel
        //     };

        //     var book = _mapper.Map<Book>(bookDto);
        //     _bookRepositoryMock.Setup(repo => repo.GetBookById(bookId)).ReturnsAsync(book);

        //     _bookRepositoryMock.Setup(repo => repo.UpdateBook(bookId, It.IsAny<Book>())).ReturnsAsync(updatedBook);

        //     var bookService = new BookService(_mapper, _bookRepositoryMock.Object);

        //     // Act
        //     var result = await bookService.UpdateBook(bookId, bookDto);

        //     // Assert
        //     Assert.NotNull(result);
        //     Assert.IsType<BookDto>(result);
        //     Assert.Equal(updatedBook.Title, result.Title);
        // }
    }
}