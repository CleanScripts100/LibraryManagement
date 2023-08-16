using Microsoft.AspNetCore.Mvc;
using WebApi.Business.src.Dto;
using WebApi.Business.src.Services.Abstractions.ServiceAbractions;
using WebApi.Domain.src.Entities;

namespace WebApi.Controller.src.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController (IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpPost]
        public BookDto AddBook([FromBody] BookDto book)
        {
            return _bookService.AddBook(book);
        }

        [HttpGet("{id}")]
        public BookDto GetBookById(Guid id)
        {
            return _bookService.GetBookById(id);
        }

        [HttpPatch("{id}")]
        public BookDto UpdateBook([FromRoute] Guid id, [FromBody] BookDto update)
        {
            return _bookService.UpdateBook(id, update);
        }

        [HttpDelete("{id}")]
        public BookDto DeleteById(Guid id)
        {
            return _bookService.DeleteBook(id);
        }
    }
}