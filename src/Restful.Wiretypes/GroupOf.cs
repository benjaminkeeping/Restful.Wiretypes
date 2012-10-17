using System.Collections.Generic;

namespace Restful.Wiretypes
{
    public class GroupOf<T>
    {
        public GroupOf()
        {
            Items = new List<T>();
        }
        public string GroupName { get; set; }
        public string GroupId { get; set; }
        public List<T> Items { get; set; }

        public void Add(T item)
        {
            Items.Add(item);
        }
    }
}