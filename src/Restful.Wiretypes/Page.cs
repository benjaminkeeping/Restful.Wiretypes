using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Restful.Wiretypes
{
    public class Page<T>
    {
        public Page()
            : this(0, 0, 0, 0, "", "", new List<T>())
        {
            Links = new List<Link>();
        }

        public IList<Link> Links { get; set; }
        public Page(int currentPage, int totalItems, int totalPages, int pageSize, string query, string pathAndQuery, IEnumerable<T> items)
        {
            Links = new List<Link>();
            pathAndQuery = StripPagingInfoFrom(pathAndQuery);
            var querySeperator = pathAndQuery.Contains("?") ? "&" : "?";
            PageInfo = new PageInfo
            {
                Query = query,
                TotalItems = totalItems,
                TotalPages = totalPages,
                CurrentPage = currentPage,
                PageSize = pageSize,
                IsLastPage = currentPage == totalPages || currentPage > totalPages,
                IsFirstPage = currentPage == 1,
                Next = currentPage == totalPages || currentPage > totalPages ? currentPage : currentPage + 1,
                Previous = currentPage == 1 ? currentPage : currentPage - 1,
                PageNumbers = Enumerable.Range(1, totalPages).ToList() /* do not remove the ToList() - causes JsonConvert to barf */
            };
            Items = items;

            Links.Add(new Link { Title = "self", Href = string.Format("{0}{1}page={2}&size={3}", pathAndQuery, querySeperator, CurrentPage, PageSize) });
            Links.Add(Link.From("<<", string.Format("{0}{1}page={2}&size={3}", pathAndQuery, querySeperator, Previous, PageSize), false, PageInfo.IsFirstPage));
            foreach (var pageNumber in PageInfo.PageNumbers)
            {
                Links.Add(Link.From(pageNumber.ToString(), string.Format("{0}{1}page={2}&size={3}", pathAndQuery, querySeperator, pageNumber, PageSize), pageNumber == PageInfo.CurrentPage, false));
            }
            Links.Add(Link.From(">>", string.Format("{0}{1}page={2}&size={3}", pathAndQuery, querySeperator, Next, PageSize), false, PageInfo.IsLastPage));
        }

        static string StripPagingInfoFrom(string pathAndQuery)
        {
            return string.Join("?", pathAndQuery.Split(new[] { '?', '&' }).Where(x => !x.StartsWith("page=") && !x.StartsWith("size=")));
        }

        public string FormatNext(string baseUrl)
        {
            return string.Format("{0}{1}{2}page={3}&size={4}", baseUrl, StripPagingInfoFrom(PageInfo.Query), PageInfo.Query.Contains("?") ? "&" : "?", Next, PageSize);
        }

        public string FormatPageNumber(int pageNumber, string baseUrl)
        {
            return string.Format("{0}{1}{2}page={3}&size={4}", baseUrl, StripPagingInfoFrom(PageInfo.Query), PageInfo.Query.Contains("?") ? "&" : "?", pageNumber, PageSize);
        }

        public string FormatPrevious(string baseUrl)
        {
            return string.Format("{0}{1}{2}page={3}&size={4}", baseUrl, StripPagingInfoFrom(PageInfo.Query), PageInfo.Query.Contains("?") ? "&" : "?", Previous, PageSize);
        }

        public int Next
        {
            get { return PageInfo.Next; }
        }

        public int Previous
        {
            get { return PageInfo.Previous; }
        }

        public int TotalItems
        {
            get { return PageInfo.TotalItems; }
        }

        public int TotalPages
        {
            get { return PageInfo.TotalPages; }
        }

        public int PageSize
        {
            get { return PageInfo.PageSize; }
        }

        public bool IsLastPage
        {
            get { return PageInfo.IsFirstPage; }
        }

        public int CurrentPage
        {
            get { return PageInfo.CurrentPage; }
        }

        [IgnoreDataMember]
        public IEnumerable<int> PageNumbers
        {
            get { return PageInfo.PageNumbers; }
        }

        public bool IsFirstPage
        {
            get { return PageInfo.IsFirstPage; }
        }

        public PageInfo PageInfo { get; set; }
        public IEnumerable<T> Items { get; set; }

        public void ReplaceItemsWith(IEnumerable<T> items)
        {
            Items = items;
        }
    }
}