using System.ComponentModel.DataAnnotations;

namespace WebApi.Business.src.Dto
{
    public class ReviewDto
    {
        public Guid BookId { get; set; }
        public Guid UserId { get; set; }
        [Range(1, 5, ErrorMessage = "The {0} field must be between {1} and {5}.")]
        public int Rating { get; set; }
        public string? Comment { get; set; }
    }
}