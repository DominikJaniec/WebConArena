using System;
using System.Collections.Generic;
using System.Linq;

namespace EternalRacer
{
    public static class IEnumerableExtensions
    {
        private static Random Randomer = new Random();

        public static T RandomOne<T>(this IEnumerable<T> collection)
        {
            return collection.ElementAt(Randomer.Next(collection.Count()));
        }
    }
}
