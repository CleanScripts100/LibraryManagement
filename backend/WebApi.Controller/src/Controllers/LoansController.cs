using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Business.src.Abstractions;
using WebApi.Business.src.Dto;

namespace WebApi.Controller.src.Controllers
{
     [ApiController]
    [Route("api/v1/[controller]")]
    public class LoansController : ControllerBase
    {
        private readonly ILoanService _loanService;
        public LoansController(ILoanService loanService)
        {
            _loanService = loanService;
        }

        [HttpPost]
        public async Task<ActionResult<LoanViewDto>> LoanBook(Guid userId, [FromBody] List<Guid> booksId)
        {
            return await _loanService.LoanBook(userId, booksId);
        }

        [HttpPost("return")]
        public async Task<ActionResult<bool>> ReturnLoanBook(Guid userId, Guid loanId)
        {
            return await _loanService.ReturnLoanedBooks(userId, loanId);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<LoanViewDto>>> GetAllLoanedBooks()
        {
            var result = await _loanService.GetAllLoanedBooks();
            return Ok(result);
        }

        [HttpGet("user-loans")]
        public async Task<ActionResult<IEnumerable<LoanViewDto>>> GetUserLoanedBooks(Guid userId)
        {
            var result = await _loanService.GetUserLoanedBooks(userId);
            return Ok(result);

        }
    }
}