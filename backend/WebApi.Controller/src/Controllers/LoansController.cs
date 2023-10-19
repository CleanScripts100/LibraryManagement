using System.Security.Claims;
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

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<LoanViewDto>> LoanBook([FromBody] List<Guid> booksId)
        {
            var userId = GetUserId();
            return await _loanService.LoanBook(userId, booksId);
        }

        [Authorize]
        [HttpPost("return/{loanId}")]
        public async Task<ActionResult<bool>> ReturnLoanBook( Guid loanId)
        {
            var userId = GetUserId();
            return await _loanService.ReturnLoanedBooks(userId, loanId);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<LoanViewDto>>> GetAllLoanedBooks()
        {
            var result = await _loanService.GetAllLoanedBooks();
            return Ok(result);
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<LoanViewDto>>> GetUserLoanedBooks(Guid userId)
        {
            var result = await _loanService.GetUserLoanedBooks(userId);
            return Ok(result);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public Guid GetUserId () 
        {
            var claim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            return new Guid (claim!.Value);
        }
    }
}