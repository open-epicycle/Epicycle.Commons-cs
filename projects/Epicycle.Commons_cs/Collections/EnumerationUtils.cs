using System.Collections.Generic;
using System.Linq;

// Authors: untrots

namespace Epicycle.Commons.Collections
{
    /// <summary>
    /// Contains various IEnumerable and IEnumerator related utilities.
    /// </summary>
    public static class EnumerationUtils
    {
        /// <summary>
        /// Concatinates all the enumerables into one enumerable.
        /// </summary>
        /// <typeparam name="T">The type of the enumerated object</typeparam>
        /// <param name="enumerables">The enumerables to concatinate. Must not be null or contain null enumerables</param>
        /// <returns>The concatinated enumerable</returns>
        public static IEnumerable<T> Concat<T>(IEnumerable<IEnumerable<T>> enumerables)
        {
            ArgAssert.NoNullIn(enumerables, "enumerables");

            var result = Enumerable.Empty<T>();

            foreach (var enumerable in enumerables)
            {
                result = result.Concat(enumerable);
            }

            return result;
        }
    }
}
