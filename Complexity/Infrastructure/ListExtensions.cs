using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure
{
    public static class ArrayExtensions
    {
        public static void Deconstruct<T>(this T[] array, out T first, out T second)
        {
            if (array is null)
                throw new ArgumentException("Null can not be deconstructed");
            if (array.Length != 2)
                throw new ArgumentException($"Amount of items in array is not 2 - it is {array.Length}");
            first = array[0];
            second = array[1];
        }
        public static void Deconstruct<T>(this T[] array, out T first, out T second, out T third)
        {
            if (array is null)
                throw new ArgumentException("Null can not be deconstructed");
            if (array.Length != 3)
                throw new ArgumentException($"Amount of items in array is not 3 - it is {array.Length}");
            first = array[0];
            second = array[1];
            third = array[2];
        }
    }
    public static class EnumerableRandomExtensions
    {
        private static IEnumerable<T> Shuffle<T>(this IEnumerable<T> enumerable, Random random)
        {
            random = random ?? new Random(42);
            return enumerable.OrderBy(_ => random.Next());
        }

        public static IEnumerable<T> TakeRandom<T>(
            this IEnumerable<T> enumerable,
            int amount = 1,
            Random random = default)
        {
            return enumerable.Shuffle(random).Take(amount);
        }

        public static T TakeRandomOne<T>(
            this IEnumerable<T> enumerable,
            Random random = default)
        {
            return enumerable.Shuffle(random).Take(1).First();
        }
    }
}