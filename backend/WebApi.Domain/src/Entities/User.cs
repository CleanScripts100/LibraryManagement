using WebApi.Domain.src.Enums;

namespace WebApi.Domain.src.Entities
{
    public class User : BaseEntity
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public Gender Gender { get; set; }
        public Role Role { get; set; }
        public string? Image { get; set; }
        public byte[]? Salt { get; set; }

    }

}