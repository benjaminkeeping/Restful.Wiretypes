using System.Collections.Generic;

namespace Restful.Wiretypes
{
    public class Field<T>
    {
        public T Value { get; set; }
        public bool Viewable { get; set; }
        public bool Editable { get; set; }
        public List<T> Options { get; set; }

        public static Field<T> From<T>(T value, bool editable, bool viewable, List<T> options)
        {
            return new Field<T> { Editable = editable, Viewable = viewable, Value = value, Options = options };
        }

        public static Field<T> From<T>(T value, bool editable, List<T> options)
        {
            return Field<T>.From(value, editable, true, options);
        }

        public static Field<T> From(T value, bool editable)
        {
            return Field<T>.From(value, editable, new List<T>());
        }

        public static Field<T> From(T value, bool editable, bool viewable)
        {
            return Field<T>.From(value, editable, viewable, new List<T>());
        }

        public override string ToString()
        {
            return Value == null ? "" : Value.ToString();
        }
    }
}