
namespace WebApi.Domain.src.Entities
{
    public class Loan
        {
        public Guid Loan_Id { get; set; }
        public int Book_Id { get; set; }
        public int User_Id { get; set; }
        public DateTime Return_Date { get; set; }
        public int Status_Id { get; set; }
    }
}