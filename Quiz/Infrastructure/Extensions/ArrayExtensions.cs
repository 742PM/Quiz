using System;
using System.Linq;

namespace Infrastructure.Extensions
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
        public static void Deconstruct<T>(this T[] array, out T first, out T second, out T third, out T forth)
        {
            if (array is null)
                throw new ArgumentException("Null can not be deconstructed");
            if (array.Length != 4)
                throw new ArgumentException($"Amount of items in array is not 4 - it is {array.Length}");
            first = array[0];
            second = array[1];
            third = array[2];
            forth = array[3];
        }

        public static T[] Shuffle<T>(this T[] array, Random random = default)
        {
            random = random ?? new Random();
            return array.OrderBy(_ => random.Next()).ToArray();
        }

    }
}