using AutoMapper;
using WebApi.Business.src.Dto;
using WebApi.Business.src.Services.Abstractions.RepoAbstractions;
using WebApi.Business.src.Services.Abstractions.ServiceAbractions;
using WebApi.Business.src.Shared;
using WebApi.Domain.src.Entities;

namespace WebApi.Business.src.Services.ServicesImplementations
{
    public class BookService : IBookService
    {
        private readonly IMapper _mapper;
        private readonly IBookRepository _bookRepository;
        public BookService(IMapper mapper, IBookRepository bookRepo)
        {
            _mapper = mapper;
            _bookRepository = bookRepo;
        }

        public async Task<BookDto> UpdateBook(Guid id, BookDto bookdto)
        {
            var findBook = await _bookRepository.GetBookById(id);
            var book = _mapper.Map<Book>(bookdto);
            if (findBook != null)
            {
                
                var updateBook = await  _bookRepository.UpdateBook(id, book);
                return _mapper.Map<BookDto>(findBook);
            } else
            {
                return null;
            }
        }

        async Task<BookDto> IBookService.AddBook(BookDto bookDto)
        {
            var bookAdd = _mapper.Map<Book>(bookDto);
            var addedbook = await _bookRepository.AddBook(bookAdd);
            return _mapper.Map<BookDto>(addedbook);        
        }

       async Task<BookDto> IBookService.DeleteBook(Guid bookId)
        {
            var deleteBook = await _bookRepository.DeleteBook(bookId);
            return _mapper.Map<BookDto>(deleteBook);        
        }

       async Task<IEnumerable<BookDto>> IBookService.GetAllBooks()
        {
            var books = await _bookRepository.GetAllBooks();
            return books.Select(books => _mapper.Map<BookDto>(books));        
        }

       async Task<BookDto> IBookService.GetBookById(Guid id)
        {
            var foundBook = await  _bookRepository.GetBookById(id);
            return _mapper.Map<BookDto>(foundBook);        
        }

        public async Task<Book> AddReview(ReviewDto dto)
        {
            var review = _mapper.Map<Review>(dto);
            if (review == null)
            {
                throw CustomException.NotFoundException();
            }
            var book = await _bookRepository.AddReview(review);
            return book;
        }

        public async Task<BookDto> LoanBook(Guid UserId, List<Guid> BooksId)
        {
            var book = await _bookRepository.LoanBook(UserId, BooksId);
           return _mapper.Map<BookDto>(book);
        }

        public async Task<BookDto> ReturnLoanedBooks(Guid UserId, Guid LoanId)
        {
            var book = await _bookRepository.ReturnLoanedBooks(UserId, LoanId);
            return _mapper.Map<BookDto>(book);
        }
    }
}