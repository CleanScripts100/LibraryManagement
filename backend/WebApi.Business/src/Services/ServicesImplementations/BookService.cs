using AutoMapper;
using WebApi.Business.src.Dto;
using WebApi.Business.src.Services.Abstractions.RepoAbstractions;
using WebApi.Business.src.Services.Abstractions.ServiceAbractions;
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
        BookDto IBookService.AddBook(BookDto bookDto)
        {
            var bookAdd = _mapper.Map<Book>(bookDto);
            var addedbook = _bookRepository.AddBook(bookAdd);
            return _mapper.Map<BookDto>(addedbook);        
        }

        BookDto IBookService.DeleteBook(Guid bookId)
        {
            var deleteBook = _bookRepository.DeleteBook(bookId);
            return _mapper.Map<BookDto>(deleteBook);        
        }

        IEnumerable<BookDto> IBookService.GetAllBooks()
        {
            var books = _bookRepository.GetAllBooks();
            return books.Select(books => _mapper.Map<BookDto>(books));        
        }

        BookDto IBookService.GetBookById(Guid id)
        {
            var foundBook = _bookRepository.GetBookById(id);
            return _mapper.Map<BookDto>(foundBook);        
        }

        BookDto? IBookService.UpdateBook(Guid bookId, BookDto bookDto)
        {
            var findBook = _bookRepository.GetBookById(bookId);
            if (findBook != null)
            {
                var updateBook = _mapper.Map<Book>(bookDto);
                findBook = _bookRepository.UpdateBook(bookId, updateBook);
                return _mapper.Map<BookDto>(findBook);
            } else
            {
                return null;
            }
        }
    }
}