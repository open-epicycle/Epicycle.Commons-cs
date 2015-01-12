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

        public static Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8>> Create<T1, T2, T3, T4, T5, T6, T7, T8>(
            T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8)
        {
            return new Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8>>(item1, item2, item3, item4, item5, item6, item7, Create(item8));
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

    public class Tuple<T1, T2, T3, T4, T5, T6, T7, TRest> : IComparable
    {
        private readonly T1 _item1;
        private readonly T2 _item2;
        private readonly T3 _item3;
        private readonly T4 _item4;
        private readonly T5 _item5;
        private readonly T6 _item6;
        private readonly T7 _item7;
        private readonly TRest _rest;

        public Tuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, TRest rest)
        {
            // TODO: Validate that TRest is one of the Tuples

            _item1 = item1;
            _item2 = item2;
            _item3 = item3;
            _item4 = item4;
            _item5 = item5;
            _item6 = item6;
            _item7 = item7;
            _rest = rest;
        }

        public T1 Item1
        {
            get { return _item1; }
        }

        public T2 Item2
        {
            get { return _item2; }
        }

        public T3 Item3
        {
            get { return _item3; }
        }

        public T4 Item4
        {
            get { return _item4; }
        }

        public T5 Item5
        {
            get { return _item5; }
        }

        public T6 Item6
        {
            get { return _item6; }
        }

        public T7 Item7
        {
            get { return _item7; }
        }

        public TRest Rest
        {
            get { return _rest; }
        }

        public override bool Equals(Object obj)
        {
            if (obj == null || !(obj is Tuple<T1, T2, T3, T4, T5, T6, T7, TRest>))
            {
                return false;
            }

            var other = (Tuple<T1, T2, T3, T4, T5, T6, T7, TRest>)obj;

            return
                Item1.Equals(other.Item1) &&
                Item2.Equals(other.Item2) &&
                Item3.Equals(other.Item3) &&
                Item4.Equals(other.Item4) &&
                Item5.Equals(other.Item5) &&
                Item6.Equals(other.Item6) &&
                Item7.Equals(other.Item7) &&
                Rest.Equals(other.Rest);
        }

        public override int GetHashCode()
        {
            return
                Item1.GetHashCode() ^
                Item2.GetHashCode() ^
                Item3.GetHashCode() ^
                Item4.GetHashCode() ^
                Item5.GetHashCode() ^
                Item6.GetHashCode() ^
                Item7.GetHashCode() ^
                Rest.GetHashCode();
        }

        public override string ToString()
        {
            var rest = Rest.ToString();

            return string.Format("({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7})",
                Item1, Item2, Item3, Item4, Item5, Item6, Item7, 
                rest.Substring(1, rest.Length - 2));
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

            if (!(obj is Tuple<T1, T2, T3, T4, T5, T6, T7, TRest>))
            {
                throw new ArgumentException(string.Format("obj is of the wrong type: {0}!", obj.GetType()));
            }

            var other = (Tuple<T1, T2, T3, T4, T5, T6, T7, TRest>)obj;

            var result = ((IComparable)Item1).CompareTo(other.Item1);
            if (result == 0)
            {
                result = ((IComparable)Item2).CompareTo(other.Item2);
                if (result == 0)
                {
                    result = ((IComparable)Item3).CompareTo(other.Item3);
                    if (result == 0)
                    {
                        result = ((IComparable)Item4).CompareTo(other.Item4);
                        if (result == 0)
                        {
                            result = ((IComparable)Item5).CompareTo(other.Item5);
                            if (result == 0)
                            {
                                result = ((IComparable)Item6).CompareTo(other.Item6);
                                if (result == 0)
                                {
                                    result = ((IComparable)Item7).CompareTo(other.Item7);
                                    if (result == 0)
                                    {
                                        return ((IComparable)Rest).CompareTo(other.Rest);
                                    }

                                    return result;
                                }

                                return result;
                            }

                            return result;
                        }

                        return result;
                    }

                    return result;
                }

                return result;                
            }

            return result;
        }
    }
}

#endif
