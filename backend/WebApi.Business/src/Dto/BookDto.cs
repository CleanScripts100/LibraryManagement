
using WebApi.Domain.src.Enums;

namespace WebApi.Business.src.Dto
{
    public class BookDto
    {
        public required List<string> Author { get; set; }
        public List<string>? Images { get; set; }
        public string? Title { get; set; }
        public string? ISBN { get; set; }
        public string? PublishedYear { get; set; }
        public string? Description { get; set; }
        public int PageCount { get; set; }
        public int InventoryCount { get; set; }
        public string? Genre { get; set; }
    }
}