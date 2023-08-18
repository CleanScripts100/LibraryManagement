
using WebApi.Domain.src.Enums;

namespace WebApi.Domain.src.Entities
{
    public class Book : BaseEntity
    {
        public string? Title { get; set; }
        public required List<string> Author { get; set; }
        public string? ISBN { get; set; }
        public String? PublishedYear { get; set; }
        public string? Description { get; set; }
        public int PageCount { get; set; }
        public Genre Genre { get; set; }
        public required List<string> Images { get; set; }
        public int InventoryCount { get; set; }
        public List<Review>? Reviews { get; set; }
    }
}