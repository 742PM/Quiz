using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure
{
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
    public static class EnumerableRandomExtensions
    {
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> enumerable, Random random = default)
        {
            random = random ?? new Random(42);
            return enumerable.OrderBy(_ => random.Next());
        }

        public static IEnumerable<T> TakeRandom<T>(this IEnumerable<T> enumerable, int amount = 1,
            Random random = default)
        {
            return enumerable.Shuffle(random).Take(amount);
        }
        public static T TakeRandomOne<T>(this IEnumerable<T> enumerable,
            Random random = default)
        {
            return enumerable.Shuffle(random).Take(1).First();
        }
    }
}