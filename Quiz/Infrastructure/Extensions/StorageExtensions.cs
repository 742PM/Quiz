using System;

namespace Infrastructure.Extensions
{
    public static class StorageExtensions
    {
        public static T Identity<T>(this T item) => item;

        public static Storage[] MapMany(this Storage[] items, Func<string[], string[]> mapper) =>
            Storage.MapMany(items, mapper);
    }
}