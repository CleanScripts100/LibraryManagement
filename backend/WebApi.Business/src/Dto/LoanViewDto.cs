
namespace WebApi.Business.src.Dto
{
    public class LoanViewDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime ReturnDate { get; set; }
        public string? Status { get; set; }
        public List<BookDto> books { get; set; }
    }
}