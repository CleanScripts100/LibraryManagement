using WebApi.Domain.src.Enums;

namespace WebApi.Business.src.Dto
{
    public class BookTitleDto
    {
        public string? Title { get; set; }
        public string? UserFullName { get; set; }
        public DateTime RturnDate { get; set; }
        public LoanStatus LoanStatus { get; set; }
    }
}
