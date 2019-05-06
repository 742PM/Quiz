using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure
{
    /// <inheritdoc />
    /// <summary>
    ///     This is a flag that means this property or object must be saved somewhere.
    ///     It does not matter how, one can create mirror-objects or serialize it by himself.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class | AttributeTargets.Struct)]
    public class MustBeSavedAttribute : Attribute
    {
    }

    public static class ListExtensions
    {
       
        public static void Deconstruct<T>(this IList<T> list, out T first, out IList<T> rest)
        {
            first = list[0];

            if (list.Count == 1)
            {
                rest = new List<T>();
                return;
            }
            rest = list.Skip(1).ToList();
        }
        public static void Deconstruct<T>(this IList<T> list, out T first, out T second, out T third)
        {
            if (list.Count != 3)
                throw new ArgumentException("Wrong amount of elements in list");
            first = list[0];
            second = list[1];
            third = list[2];
        }
    }
}
