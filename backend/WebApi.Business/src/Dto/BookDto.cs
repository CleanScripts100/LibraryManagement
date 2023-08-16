
using WebApi.Domain.src.Enums;

namespace WebApi.Business.src.Dto
{
    public class BookDto
    {
        public required List<string> Author { get; set; }
        public List<string>? Images { get; set; }
        public string? Title { get; set; }
        public string? ISBN { get; set; }
        public String? PublishedYear { get; set; }
        public string? Description { get; set; }
        public Genre Genre { get; set; }
    }
}