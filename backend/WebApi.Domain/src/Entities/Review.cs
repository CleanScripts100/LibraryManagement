
namespace WebApi.Domain.src.Entities
{
    public class Review : BaseEntity
    {
        public Guid BookId { get; set; }
        public Guid UserId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
    }
}