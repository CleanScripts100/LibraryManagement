
namespace WebApi.Domain.src.Shared
{
    public class QueryOptions
    {
        public string? Search {get; set;}
        public string? Order {get; set;}
        public bool OrderByDesc {get; set;} = false;
        public int PageNumber {get; set;} = 1;
        public string? PerPage {get; set;}
    }
}