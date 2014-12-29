using System.Collections.Generic;

namespace Epicycle.Commons.Collections
{
    public static class EmptyList<T>
    {
        public static readonly IReadOnlyList<T> Instance = new T[0];
    }
}
