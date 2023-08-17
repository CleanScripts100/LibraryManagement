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

        public async Task<BookDto> UpdateBook(Guid id, Book book)
        {
            var findBook = await _bookRepository.GetBookById(id);
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
    }
}