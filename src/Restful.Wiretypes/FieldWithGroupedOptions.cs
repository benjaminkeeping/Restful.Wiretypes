using System.Collections.Generic;
using System.Linq;

namespace Restful.Wiretypes
{
    public class FieldWithGroupedOptions<T>
    {
        public T Value { get; set; }
        public bool Viewable { get; set; }
        public bool Editable { get; set; }
        public List<GroupOf<KeyValuePair<T, T>>> Options { get; set; }

        public static FieldWithGroupedOptions<T> From(T value, bool editable, bool viewable, List<GroupOf<KeyValuePair<T, T>>> options)
        {
            return new FieldWithGroupedOptions<T> { Editable = editable, Viewable = viewable, Value = value, Options = options };
        }

        public static FieldWithGroupedOptions<T> From(T value, bool editable, List<GroupOf<KeyValuePair<T, T>>> options)
        {
            return FieldWithGroupedOptions<T>.From(value, editable, true, options);
        }

        public static FieldWithGroupedOptions<T> From(T value, bool editable)
        {
            return FieldWithGroupedOptions<T>.From(value, editable, new List<GroupOf<KeyValuePair<T, T>>>());
        }

        public static FieldWithGroupedOptions<T> From(T value, bool editable, bool viewable)
        {
            return FieldWithGroupedOptions<T>.From(value, editable, viewable, new List<GroupOf<KeyValuePair<T, T>>>());
        }

        public override string ToString()
        {
            return Value == null ? "" : Value.ToString();
        }

    }
}