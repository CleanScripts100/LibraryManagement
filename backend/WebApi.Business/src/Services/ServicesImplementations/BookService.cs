using AutoMapper;
using WebApi.Business.src.Dto;
using WebApi.Business.src.Services.Abstractions.ServiceAbractions;
using WebApi.Domain.src.Abstractions;
using WebApi.Domain.src.Entities;
using WebApi.Domain.src.Shared;

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
                _ = await _bookRepository.UpdateBook(id, book);
                return _mapper.Map<BookDto>(findBook);
            } else
            {
                return null;
            }
        }

        public async Task<BookDto> AddBook(BookDto bookDto)
        {
            var bookAdd = _mapper.Map<Book>(bookDto);
            var addedbook = await _bookRepository.AddBook(bookAdd);
            return _mapper.Map<BookDto>(addedbook);        
        }

        public async Task<BookDto> DeleteBook(Guid bookId)
        {
            var deleteBook = await _bookRepository.DeleteBook(bookId);
            return _mapper.Map<BookDto>(deleteBook);        
        }

        public async Task<IEnumerable<BookReadDto>> GetAllBooks()
        {
            var books = await _bookRepository.GetAllBooks();
            return books.Select(books => _mapper.Map<BookReadDto>(books));        
        }

        public async Task<IEnumerable<BookReadDto>> GetAllBooks(QueryOptions queryOptions)
        {
            var result = await _bookRepository.GetAllBooks(queryOptions);
            return _mapper.Map<IEnumerable<BookReadDto>>(result);
        }

        public async Task<BookDto> GetBookById(Guid id)
        {
            var foundBook = await  _bookRepository.GetBookById(id);
            return _mapper.Map<BookDto>(foundBook);        
        }
    }
}