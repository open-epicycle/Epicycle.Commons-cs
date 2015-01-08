// [[[[INFO>
// Copyright 2015 Epicycle (http://epicycle.org, https://github.com/open-epicycle)
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// 
// For more information check https://github.com/open-epicycle/Epicycle.Commons-cs
// ]]]]

using System;
using System.Collections.Generic;
#if NET35 || NET40
using System.Collections.ObjectModel;
#endif

namespace Epicycle.Commons.Collections
{
    public static class CollectionUtils
    {
        public static IReadOnlyList<T> AsReadOnlyList<T>(this List<T> @this)
        {
#if NET35 || NET40
            return new ReadOnlyListWrapper<T>(@this);
#else
            return @this;
#endif
        }

        public static T Last<T>(this IReadOnlyList<T> @this)
        {
            return @this[@this.Count - 1];
        }

        public static void RemoveLast<T>(this IList<T> @this)
        {
            @this.RemoveAt(@this.Count - 1);
        }

        public static T ElementAtCyclic<T>(this IReadOnlyList<T> @this, int i)
        {
            return @this[i.Mod(@this.Count)];
        }

        public static IReadOnlyList<T> AsSingleton<T>(this T @this)
        {
            return new SingletonList<T>(@this);
        }

        private sealed class SingletonList<T> : IReadOnlyList<T>
        {
            public SingletonList(T item)
            {
                _item = item;
            }

            private readonly T _item;

            public int Count
            {
                get { return 1; }
            }

            public IEnumerator<T> GetEnumerator()
            {
                yield return _item;
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public T this[int index]
            {
                get 
                { 
                    if (index != 0)
                    {
                        throw new IndexOutOfRangeException(string.Format("Singleton has index 0 only but attempted to retrieve index {0}", index));
                    }

                    return _item;
                }
            }
        }

        public static IList<T> Reverse<T>(this IReadOnlyList<T> @this)
        {
            var answer = new List<T>(@this.Count);

            for (var j = @this.Count - 1; j >= 0; j--)
            {
                answer.Add(@this[j]);
            }

            return answer;
        }

        public static IReadOnlyList<T> AsReverse<T>(this IReadOnlyList<T> @this)
        {
            return new ReverseList<T>(@this);
        }

        private sealed class ReverseList<T> : IReadOnlyList<T>
        {
            public ReverseList(IReadOnlyList<T> list)
            {
                _origList = list;
            }

            private readonly IReadOnlyList<T> _origList;

            public T this[int index]
            {
                get { return _origList[_origList.Count - index - 1]; }
            }

            public int Count
            {
                get { return _origList.Count; }
            }

            public IEnumerator<T> GetEnumerator()
            {
                for (var i = _origList.Count - 1; i >= 0; i--)
                {
                    yield return _origList[i];
                }
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        public static IReadOnlyList<T> AsCyclicPermutation<T>(this IReadOnlyList<T> @this, int shift)
        {
            return new CyclicPermutedList<T>(@this, shift);
        }

        private sealed class CyclicPermutedList<T> : IReadOnlyList<T>
        {
            public CyclicPermutedList(IReadOnlyList<T> list, int shift)
            {
                _origList = list;
                _shift = shift;
            }

            private readonly IReadOnlyList<T> _origList;
            private readonly int _shift;

            public T this[int index]
            {
                get 
                {
                    if (index < 0 || index >= Count)
                    {
                        throw new IndexOutOfRangeException(string.Format("Attempted to access list at index {0} while Count is {1}", index, Count));
                    }
 
                    return _origList[(index - _shift).Mod(_origList.Count)];
                }
            }

            public int Count
            {
                get { return _origList.Count; }
            }

            public IEnumerator<T> GetEnumerator()
            {
                for (int i = 0; i < Count; i++)
                {
                    yield return this[i];
                }
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        // indices are assumed to be sorted
        public static void SieveByIndex<T>(this IReadOnlyList<T> @this, IEnumerable<int> indices, ICollection<T> inside, ICollection<T> outside)
        {
            var idxEnum = indices.GetEnumerator();
            var validIndex = idxEnum.MoveNext();

            for (var i = 0; i < @this.Count; i++)
            {
                if (!validIndex || idxEnum.Current > i)
                {
                    outside.Add(@this[i]);
                }
                else
                {
                    inside.Add(@this[i]);
                    validIndex = idxEnum.MoveNext();
                }
            }
        }

        // indices are assumed to be sorted
        public static void SieveByIndex<T>(this IReadOnlyList<T> @this, IEnumerable<int> indices, ICollection<T> outside)
        {
            var idxEnum = indices.GetEnumerator();
            var validIndex = idxEnum.MoveNext();

            for (var i = 0; i < @this.Count; i++)
            {
                if (!validIndex || idxEnum.Current > i)
                {
                    outside.Add(@this[i]);
                }
                else
                {
                    validIndex = idxEnum.MoveNext();
                }
            }
        }
    }
}
