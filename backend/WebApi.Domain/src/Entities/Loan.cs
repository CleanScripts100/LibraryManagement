
using WebApi.Domain.src.Enums;

namespace WebApi.Domain.src.Entities
{
    public class Loan
        {
        public Guid LoanId { get; set; }
        public  List<Guid>? BookId { get; set; }
        public  Guid UserId { get; set; }
        public DateTime ReturnDate { get; set; }
        public LoanStatus Status { get; set; }
    }
}