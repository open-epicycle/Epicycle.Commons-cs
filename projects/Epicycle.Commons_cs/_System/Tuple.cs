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

#if NET35

namespace System
{
    // TODO: Tuples should implement IStructuralEquatable, IStructuralComparable
    // TODO: Tuples should implement int IStructuralComparable.CompareTo(Object other, IComparer comparer)
    // TODO: Tuples should implement bool IStructuralEquatable.Equals(Object other, IEqualityComparer comparer)
    // TODO: Tuples should implement int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)

    public static class Tuple
    {
        public static Tuple<T1> Create<T1>(T1 item1)
        {
            return new Tuple<T1>(item1);
        }

        public static Tuple<T1, T2> Create<T1, T2>(T1 item1, T2 item2)
        {
            return new Tuple<T1, T2>(item1, item2);
        }
    }

    public class Tuple<T1> : IComparable
    {
        private readonly T1 _item1;

        public Tuple(T1 item1)
        {
            _item1 = item1;
        }

        public T1 Item1
        {
            get { return _item1; }
        }

        public override bool Equals(Object obj)
        {
            if (obj == null || !(obj is Tuple<T1>))
            {
                return false;
            }

            var other = (Tuple<T1>)obj;

            return Item1.Equals(other.Item1);
        }

        public override int GetHashCode()
        {
            return Item1.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("({0})", Item1);
        }

        int IComparable.CompareTo(Object obj)
        {
            if (obj == (object)this)
            {
                return 0;
            }

            if (obj == null)
            {
                return 1;
            }

            if (!(obj is Tuple<T1>))
            {
                throw new ArgumentException(string.Format("obj is of the wrong type: {0}!", obj.GetType()));
            }

            var other = (Tuple<T1>)obj;


            return ((IComparable)Item1).CompareTo(other.Item1);
        }
    }

    public class Tuple<T1, T2> : IComparable
    {
        private readonly T1 _item1;
        private readonly T2 _item2;

        public Tuple(T1 item1, T2 item2)
        {
            _item1 = item1;
            _item2 = item2;
        }

        public T1 Item1
        {
            get { return _item1; }
        }

        public T2 Item2
        {
            get { return _item2; }
        }

        public override bool Equals(Object obj)
        {
            if (obj == null || !(obj is Tuple<T1, T2>))
            {
                return false;
            }

            var other = (Tuple<T1, T2>)obj;

            return Item1.Equals(other.Item1) && Item2.Equals(other.Item2);
        }

        public override int GetHashCode()
        {
            return Item1.GetHashCode() ^ Item2.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("({0}, {1})", Item1, Item2);
        }

        int IComparable.CompareTo(Object obj)
        {
            if(obj == (object)this)
            {
                return 0;
            }

            if(obj == null)
            {
                return 1;
            }

            if(!(obj is Tuple<T1, T2>))
            {
                throw new ArgumentException(string.Format("obj is of the wrong type: {0}!", obj.GetType()));
            }

            var other = (Tuple<T1, T2>)obj;

            var result = ((IComparable)Item1).CompareTo(other.Item1);
            if (result == 0)
            {
                return ((IComparable)Item2).CompareTo(other.Item2);
            }

            return result;
        }
    }
}

#endif
