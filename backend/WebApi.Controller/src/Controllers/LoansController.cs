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
        public async Task<ActionResult<LoanViewDto>> LoanBook(Guid UserId, [FromBody] List<Guid> BooksId)
        {
            return await _loanService.LoanBook(UserId, BooksId);
        }

        [HttpPost("returnloan")]
        public async Task<ActionResult<bool>> ReturnLoanBook(Guid UserId, Guid LoanId)
        {
            return await _loanService.ReturnLoanedBooks(UserId, LoanId);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<LoanViewDto>>> GetAllLoanedBooks()
        {
            var result = await _loanService.GetAllLoanedBooks();
            return Ok(result);
        }

        [HttpGet("userloaned")]
        public async Task<ActionResult<IEnumerable<LoanViewDto>>> GetUserLoanedBooks(Guid UserId)
        {
            var result = await _loanService.GetUserLoanedBooks(UserId);
            return Ok(result);

        }
    }
}