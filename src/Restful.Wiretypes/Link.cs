namespace Restful.Wiretypes
{
    public class Link
    {
        public bool IsActive { get; set; }
        public bool IsDisabled { get; set; }
        public string Title { get; set; }
        public string Href { get; set; }

        public static Link From(string title, string href)
        {
            return From(title, href, false, false);
        }

        public static Link From(string title, string href, bool isActive, bool isDisabled)
        {
            return new Link { Title = title, Href = href, IsActive = isActive, IsDisabled = isDisabled };
        }
    }
}