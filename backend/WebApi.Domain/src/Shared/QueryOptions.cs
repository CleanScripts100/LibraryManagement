
namespace WebApi.Domain.src.Shared
{
    public class QueryOptions
    {
        public SearchParams? Search {get; set;}
        public bool OrderByDesc {get; set;} = false;
        public int? PageNumber {get; set;}
        public int? PerPage {get; set;}
        

        public class SearchParams
        {
            public SearchKey SearchKey { get; set; }
            public string SearchKeyValue { get; set; } = null!;
        }

        public enum SearchKey
        {
            Title = 1,
            Author,
            Genre
        }
    }
}