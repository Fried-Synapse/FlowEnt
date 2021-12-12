using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace FriedSynapse.Quickit
{
    public static class EnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> enumeration, Expression<Action<T>> predicate)
        {
            if (enumeration == null)
            {
                throw new ArgumentException("Enumeration cannot be null.", nameof(enumeration));
            }

            if (predicate == null)
            {
                throw new ArgumentException("Predicate cannot be null.", nameof(predicate));
            }
            Action<T> action = predicate.Compile();
            foreach (T item in enumeration)
            {
                action(item);
            }
        }
    }
}
