
namespace WebApi.Business.src.Dto
{
    public class LoanDto
    {
        public Guid LoanId { get; set; }
        public Guid UserId { get; set; }
        public DateTime ReturnDate { get; set; }
        public string? Status { get; set; }
        public List<BookDto>? Books { get; set; } 
    }
}