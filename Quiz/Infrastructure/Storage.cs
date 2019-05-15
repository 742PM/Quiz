using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text.RegularExpressions;
using Infrastructure.Extensions;
using static System.String;

namespace Infrastructure
{
    public sealed class Storage : IEquatable<Storage>
    {
        private Storage(ICollection<string> items)
        {
            if (items == null)
                throw new ArgumentException("Can not split null");
            Key = Guid.NewGuid().ToString();
            Count = items.Count - 1;
            Concatenated = items.Count == 0 ? "" : Join(Key, items);
        }

        private Storage(string value, string key, int count) => (Concatenated, Key, Count) = (value, key, count);

        private int Count { get; }

        private string Concatenated { get; }

        private string Key { get; }

        public bool Equals(Storage other) => Split().SequenceEqual(other.Split());

        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj is Storage storage)
                return Equals(storage);
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Concatenated?.GetHashCode() ?? 0) * 397) ^ (Key?.GetHashCode() ?? 0);
            }
        }

        public static Storage Concat(params string[] items) => new Storage(items);

        [Pure]
        public string[] Split() => Concatenated?.Length == 0
                                       ? new string[0]
                                       : Concatenated?.Split(Key)?.ToArray() ?? new string[0];

        /// <summary>
        ///     Применяет функцию к "склеенным" в строку входным элементам.
        /// </summary>
        /// <param name="mapper">
        ///     Функция, изменяющая элементы.
        ///     Функция не должна увеличивать вероятность разделителя (<see cref="Guid" />) попасть в элементы
        /// </param>
        [Pure]
        public Storage Map(Func<string, string> mapper)
        {
            var mapped = mapper(Concatenated);
            var newKey = mapper(Key);
            var newCount = Regex.Matches(mapped, newKey).Count;
            if (newCount != Count)
                throw new ArgumentException($"Your function {mapper.Method.Name} is breaking Storage law");
            return new Storage(mapped, newKey, Count);
        }

        public override string ToString() => $"Contains {Join(", ", Split())};" +
                                             $" {nameof(Count)}: {Count}, {nameof(Concatenated)}: {Concatenated}, {nameof(Key)}: {Key}";

        [Pure]
        public static Storage[] MapMany(Storage[] items, Func<string[], string[]> mapper) =>
            items.Zip(mapper(items.Select(storage => storage.Concatenated)
                                  .ToArray()), mapper(items.Select(storage => storage.Key).ToArray()),
                      (storage, mappedValue, mappedKey) => new Storage(mappedValue, mappedKey, storage.Count))
                 .ToArray();
    }
}