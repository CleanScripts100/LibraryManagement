
using AutoMapper;
using Moq;
using WebApi.Business.src.Dto;
using WebApi.Business.src.Services.ServicesImplementations;
using WebApi.Domain.src.Abstractions;
using WebApi.Domain.src.Entities;
using WebApi.Domain.src.Enums;
using WebApi.Infrastructure.src.Configuration;

namespace WebApi.Tests.src.Service.Tests
{
    public class LoanServiceTests
    {
        private readonly Mock<ILoanRepository> _loanRepositoryMock;
        private readonly IMapper _mapper;

        public LoanServiceTests()
        {
            _loanRepositoryMock = new Mock<ILoanRepository>();
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<MapperProfile>()).CreateMapper();
        }

        [Fact]
        public async Task GetAllLoanedBooks_ReturnsListOfLoanViewDto()
        {
            // Arrange
            var loanList = new List<Loan>
            {
                new Loan
                {
                    Id = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    ReturnDate = DateTime.Now.AddDays(7),
                    Status = LoanStatus.Borrowed,
                    Books = new List<Book>
                    {
                        new Book
                        {
                            Title = "Sample Book 1",
                            Author = new List<string> { "Author 1", "Author 2" },
                            Images = new List<string> { "image1.jpg", "image2.jpg" },
                            ISBN = "978-1234567890",
                            PublishedYear = "2012",
                            Description = "Sample description",
                            PageCount = 100,
                            InventoryCount = 10,
                            Genre = Genre.Fiction
                        }
                    }
                },

                new Loan
                {
                    Id = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    ReturnDate = DateTime.Now.AddDays(7),
                    Status = LoanStatus.Borrowed,
                    Books = new List<Book>
                    {
                        new Book
                        {
                            Title = "Sample Book 1",
                            Author = new List<string> { "Author 1", "Author 2" },
                            Images = new List<string> { "image1.jpg", "image2.jpg" },
                            ISBN = "978-1234567890",
                            PublishedYear = "2012",
                            Description = "Sample description",
                            PageCount = 100,
                            InventoryCount = 10,
                            Genre = Genre.Fiction
                        }
                    }
                },
            };
            _loanRepositoryMock.Setup(repo => repo.GetAllLoanedBooks()).ReturnsAsync(loanList);

            var loanService = new LoanService(_loanRepositoryMock.Object);

            // Act
            var result = await loanService.GetAllLoanedBooks();

            // Convert the result to a list
            var resultList = result.ToList();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<LoanViewDto>>(result);

            // Check if the count of returned loans matches the expected count
            Assert.Equal(loanList.Count, result.Count());

            // Loop through each returned loan view dto and check its properties
            for (int i = 0; i < loanList.Count; i++)
            {
                Assert.Equal(loanList[i].Id, resultList[i].Id);
                Assert.Equal(loanList[i].UserId, resultList[i].UserId);
                Assert.Equal(loanList[i].ReturnDate, resultList[i].ReturnDate);
                Assert.Equal(loanList[i].Status.ToString(), resultList[i].Status);

                // Loop through each book in the loan and check its properties
                for (int j = 0; j < loanList[i].Books?.Count; j++)
                {
                    Assert.Equal(loanList[i].Books[j].Title, resultList[i].books[j].Title);
                    Assert.Equal(loanList[i].Books[j].Author, resultList[i].books[j].Author);
                    Assert.Equal(loanList[i].Books[j].Description, resultList[i].books[j].Description);
                    Assert.Equal(loanList[i].Books[j].Genre.ToString(), resultList[i].books[j].Genre);
                }
            }
        }

        [Fact]
        public async Task LoanBook_ReturnsLoanViewDto()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var booksId = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
            var loan = new Loan
            {
                Id = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                ReturnDate = DateTime.Now.AddDays(7),
                Status = LoanStatus.Borrowed,
                Books = new List<Book>
                {
                    new Book
                    {
                        Title = "Sample Book 1",
                        Author = new List<string> { "Author 1", "Author 2" },
                        Images = new List<string> { "image1.jpg", "image2.jpg" },
                        ISBN = "978-1234567890",
                        PublishedYear = "2012",
                        Description = "Sample description",
                        PageCount = 100,
                        InventoryCount = 10,
                        Genre = Genre.Fiction
                    }
                }
            };

            _loanRepositoryMock.Setup(repo => repo.LoanBook(userId, booksId)).ReturnsAsync(loan);

            var loanService = new LoanService(_loanRepositoryMock.Object);

            // Act
            var result = await loanService.LoanBook(userId, booksId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<LoanViewDto>(result);
            Assert.Equal(loan.Id, result.Id);
            Assert.Equal(loan.UserId, result.UserId);
            Assert.Equal(loan.ReturnDate, result.ReturnDate);
            Assert.Equal(loan.Status.ToString(), result.Status);

            Assert.Equal(loan.Books.Count, result.books.Count);
            for (int i = 0; i < loan.Books.Count; i++)
            {
                Assert.Equal(loan.Books[i].Title, result.books[i].Title);
                Assert.Equal(loan.Books[i].Author, result.books[i].Author);
            }
        }

        [Fact]
        public async Task ReturnLoanedBook_ReturnsBool()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var loanId = Guid.NewGuid();
            var expectedResult = true; 
            _loanRepositoryMock.Setup(repo => repo.ReturnLoanedBooks(userId, loanId)).ReturnsAsync(expectedResult);

            var loanService = new LoanService(_loanRepositoryMock.Object);

            // Act
            var result = await loanService.ReturnLoanedBooks(userId, loanId);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public async Task GetUserLoanedBooks_ReturnsListOfLoanViewDto()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var loanList = new List<Loan>
            {
                new Loan
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    ReturnDate = DateTime.Now.AddDays(7),
                    Status = LoanStatus.Borrowed,
                    Books = new List<Book>
                    {
                        new Book
                        {
                            Title = "Sample Book 1",
                            Author = new List<string> { "Author 1", "Author 2" },
                            Images = new List<string> { "image1.jpg", "image2.jpg" },
                            ISBN = "978-1234567890",
                            PublishedYear = "2012",
                            Description = "Sample description",
                            PageCount = 100,
                            InventoryCount = 10,
                            Genre = Genre.Fiction
                        }
                    }
                },

                new Loan
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    ReturnDate = DateTime.Now.AddDays(7),
                    Status = LoanStatus.Borrowed,
                    Books = new List<Book>
                    {
                        new Book
                        {
                            Title = "Sample Book 1",
                            Author = new List<string> { "Author 1", "Author 2" },
                            Images = new List<string> { "image1.jpg", "image2.jpg" },
                            ISBN = "978-1234567890",
                            PublishedYear = "2012",
                            Description = "Sample description",
                            PageCount = 100,
                            InventoryCount = 10,
                            Genre = Genre.Fiction
                        }
                    }
                },
            };
            
            _loanRepositoryMock.Setup(repo => repo.GetUserLoanedBooks(userId)).ReturnsAsync(loanList);

            var loanService = new LoanService(_loanRepositoryMock.Object);

            // Act
            var result = await loanService.GetUserLoanedBooks(userId);

            // Convert the result to a list
            var resultList = result.ToList();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<LoanViewDto>>(result);
            Assert.Equal(2, result.Count());

            for (int i = 0; i < loanList.Count; i++)
            {
                Assert.Equal(loanList[i].Id, resultList[i].Id);
                Assert.Equal(loanList[i].UserId, resultList[i].UserId);
                Assert.Equal(loanList[i].ReturnDate, resultList[i].ReturnDate);
                Assert.Equal(loanList[i].Status.ToString(), resultList[i].Status);

                Assert.Equal(loanList[i].Books.Count, resultList[i].books.Count);
                for (int j = 0; j < loanList[i].Books.Count; j++)
                {
                    Assert.Equal(loanList[i].Books[j].Title, resultList[i].books[j].Title);
                    Assert.Equal(loanList[i].Books[j].Author, resultList[i].books[j].Author);
                }
            }
        }
    }
}