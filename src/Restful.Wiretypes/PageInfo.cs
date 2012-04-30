using System.Collections.Generic;

namespace Restful.Wiretypes
{
    public class PageInfo
    {
        public int Next { get; set; }
        public int Previous { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public bool IsLastPage { get; set; }
        public int CurrentPage { get; set; }
        public IEnumerable<int> PageNumbers { get; set; }
        public bool IsFirstPage { get; set; }

        public string Query { get; set; }
    }
}