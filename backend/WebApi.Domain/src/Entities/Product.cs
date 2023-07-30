namespace WebApi.Domain.src.Entities
{
    public class Product
    {
        public Guid id {get; set;}
        public required string Title {get; set;}
        public float Price { get ;set;}
    }
}