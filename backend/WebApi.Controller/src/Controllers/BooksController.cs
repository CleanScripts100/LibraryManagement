using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<BookDto>> AddBook([FromBody] BookDto book)
        {
            return await _bookService.AddBook(book);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookDto>> GetBookById(Guid id)
        {
            return await _bookService.GetBookById(id);
        }

        [HttpPatch("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<BookDto>> UpdateBook([FromRoute] Guid id, [FromBody] Book update)
        {
            return await  _bookService.UpdateBook(id, update);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<BookDto>> DeleteById(Guid id)
        {
            return await _bookService.DeleteBook(id);
        }
    }
}