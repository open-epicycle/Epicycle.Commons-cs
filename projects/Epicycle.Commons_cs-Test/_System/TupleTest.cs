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
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace System
{
    [TestFixture]
    public class TupleTest
    {
        private Tuple<int> _tuple1;
        private Tuple<int> _tuple1alt;
        private Tuple<int> _tuple1dif1;
        private Tuple<int> _tuple1less1;
        private Tuple<int> _tuple1great1;

        private Tuple<int, string> _tuple2;
        private Tuple<int, string> _tuple2alt;
        private Tuple<int, string> _tuple2dif1;
        private Tuple<int, string> _tuple2dif2;
        private Tuple<int, string> _tuple2less1;
        private Tuple<int, string> _tuple2less2;
        private Tuple<int, string> _tuple2great1;
        private Tuple<int, string> _tuple2great2;

        private Tuple<int, string, double, char, long, string, float, Tuple<string>> _tuple8;
        private Tuple<int, string, double, char, long, string, float, Tuple<string>> _tuple8alt;
        private Tuple<int, string, double, char, long, string, float, Tuple<string>> _tuple8dif1;
        private Tuple<int, string, double, char, long, string, float, Tuple<string>> _tuple8dif2;
        private Tuple<int, string, double, char, long, string, float, Tuple<string>> _tuple8dif3;
        private Tuple<int, string, double, char, long, string, float, Tuple<string>> _tuple8dif4;
        private Tuple<int, string, double, char, long, string, float, Tuple<string>> _tuple8dif5;
        private Tuple<int, string, double, char, long, string, float, Tuple<string>> _tuple8dif6;
        private Tuple<int, string, double, char, long, string, float, Tuple<string>> _tuple8dif7;
        private Tuple<int, string, double, char, long, string, float, Tuple<string>> _tuple8dif8;
        private Tuple<int, string, double, char, long, string, float, Tuple<string>> _tuple8less1;
        private Tuple<int, string, double, char, long, string, float, Tuple<string>> _tuple8less2;
        private Tuple<int, string, double, char, long, string, float, Tuple<string>> _tuple8less3;
        private Tuple<int, string, double, char, long, string, float, Tuple<string>> _tuple8less4;
        private Tuple<int, string, double, char, long, string, float, Tuple<string>> _tuple8less5;
        private Tuple<int, string, double, char, long, string, float, Tuple<string>> _tuple8less6;
        private Tuple<int, string, double, char, long, string, float, Tuple<string>> _tuple8less7;
        private Tuple<int, string, double, char, long, string, float, Tuple<string>> _tuple8less8;
        private Tuple<int, string, double, char, long, string, float, Tuple<string>> _tuple8great1;
        private Tuple<int, string, double, char, long, string, float, Tuple<string>> _tuple8great2;
        private Tuple<int, string, double, char, long, string, float, Tuple<string>> _tuple8great3;
        private Tuple<int, string, double, char, long, string, float, Tuple<string>> _tuple8great4;
        private Tuple<int, string, double, char, long, string, float, Tuple<string>> _tuple8great5;
        private Tuple<int, string, double, char, long, string, float, Tuple<string>> _tuple8great6;
        private Tuple<int, string, double, char, long, string, float, Tuple<string>> _tuple8great7;
        private Tuple<int, string, double, char, long, string, float, Tuple<string>> _tuple8great8;

        [SetUp]
        public void SetuUp()
        {
            _tuple1       = Tuple.Create(1);
            _tuple1alt    = Tuple.Create(1);
            _tuple1dif1   = Tuple.Create(2);
            _tuple1less1  = Tuple.Create(0);
            _tuple1great1 = Tuple.Create(2);

            _tuple2       = Tuple.Create(1, "m");
            _tuple2alt    = Tuple.Create(1, "m");
            _tuple2dif1   = Tuple.Create(2, "m");
            _tuple2dif2   = Tuple.Create(1, "X");
            _tuple2less1  = Tuple.Create(0, "m");
            _tuple2less2  = Tuple.Create(1, "a");
            _tuple2great1 = Tuple.Create(2, "m");
            _tuple2great2 = Tuple.Create(1, "z");

            _tuple8       = Tuple.Create(1, "m", 1.1, 'm', 1L, "#m", 1.1f, "*m");
            _tuple8alt    = Tuple.Create(1, "m", 1.1, 'm', 1L, "#m", 1.1f, "*m");
            _tuple8dif1   = Tuple.Create(2, "m", 1.1, 'm', 1L, "#m", 1.1f, "*m");
            _tuple8dif2   = Tuple.Create(1, "x", 1.1, 'm', 1L, "#m", 1.1f, "*m");
            _tuple8dif3   = Tuple.Create(1, "m", 1.2, 'm', 1L, "#m", 1.1f, "*m");
            _tuple8dif4   = Tuple.Create(1, "m", 1.1, 'x', 1L, "#m", 1.1f, "*m");
            _tuple8dif5   = Tuple.Create(1, "m", 1.1, 'm', 2L, "#m", 1.1f, "*m");
            _tuple8dif6   = Tuple.Create(1, "m", 1.1, 'm', 1L, "#x", 1.1f, "*m");
            _tuple8dif7   = Tuple.Create(1, "m", 1.1, 'm', 1L, "#m", 1.2f, "*m");
            _tuple8dif8   = Tuple.Create(1, "m", 1.1, 'm', 1L, "#m", 1.1f, "*x");
            _tuple8less1  = Tuple.Create(0, "m", 1.1, 'm', 1L, "#m", 1.1f, "*m");
            _tuple8less2  = Tuple.Create(1, "a", 1.1, 'm', 1L, "#m", 1.1f, "*m");
            _tuple8less3  = Tuple.Create(1, "m", 1.0, 'm', 1L, "#m", 1.1f, "*m");
            _tuple8less4  = Tuple.Create(1, "m", 1.1, 'a', 1L, "#m", 1.1f, "*m");
            _tuple8less5  = Tuple.Create(1, "m", 1.1, 'm', 0L, "#m", 1.1f, "*m");
            _tuple8less6  = Tuple.Create(1, "m", 1.1, 'm', 1L, "#a", 1.1f, "*m");
            _tuple8less7  = Tuple.Create(1, "m", 1.1, 'm', 1L, "#m", 1.0f, "*m");
            _tuple8less8  = Tuple.Create(1, "m", 1.1, 'm', 1L, "#m", 1.1f, "*a");
            _tuple8great1 = Tuple.Create(2, "m", 1.1, 'm', 1L, "#m", 1.1f, "*m");
            _tuple8great2 = Tuple.Create(1, "x", 1.1, 'm', 1L, "#m", 1.1f, "*m");
            _tuple8great3 = Tuple.Create(1, "m", 1.2, 'm', 1L, "#m", 1.1f, "*m");
            _tuple8great4 = Tuple.Create(1, "m", 1.1, 'x', 1L, "#m", 1.1f, "*m");
            _tuple8great5 = Tuple.Create(1, "m", 1.1, 'm', 2L, "#m", 1.1f, "*m");
            _tuple8great6 = Tuple.Create(1, "m", 1.1, 'm', 1L, "#x", 1.1f, "*m");
            _tuple8great7 = Tuple.Create(1, "m", 1.1, 'm', 1L, "#m", 1.2f, "*m");
            _tuple8great8 = Tuple.Create(1, "m", 1.1, 'm', 1L, "#m", 1.1f, "*x");
        }

        #region Create

        [Test]
        public void Create1_creates_tuple_with_correct_items()
        {
            Assert.That(_tuple1.Item1, Is.EqualTo(1));
        }

        [Test]
        public void Create2_creates_tuple_with_correct_items()
        {
            Assert.That(_tuple2.Item1, Is.EqualTo(1));
            Assert.That(_tuple2.Item2, Is.EqualTo("m"));
        }

        [Test]
        public void Create8_creates_tuple_with_correct_items()
        {
            Assert.That(_tuple8.Item1, Is.EqualTo(1));
            Assert.That(_tuple8.Item2, Is.EqualTo("m"));
            Assert.That(_tuple8.Item3, Is.EqualTo(1.1));
            Assert.That(_tuple8.Item4, Is.EqualTo('m'));
            Assert.That(_tuple8.Item5, Is.EqualTo(1L));
            Assert.That(_tuple8.Item6, Is.EqualTo("#m"));
            Assert.That(_tuple8.Item7, Is.EqualTo(1.1f));
            Assert.That(_tuple8.Rest.Item1, Is.EqualTo("*m"));
        }

        #endregion

        #region Equals & GetHashCode

        #region Tuple1

        [Test]
        public void Equals1_not_equals_to_null()
        {
            Assert.That(_tuple1.Equals(null), Is.False);
        }

        [Test]
        public void Equals1_not_equals_to_other_type()
        {
            Assert.That(_tuple1.Equals(123), Is.False);
        }

        [Test]
        public void Equals1_not_equals_to_different_tuple()
        {
            Assert.That(_tuple1.Equals(_tuple1dif1), Is.False);
        }

        [Test]
        public void Equals1_equals_to_similar_tuple()
        {
            Assert.That(_tuple1.Equals(_tuple1alt), Is.True);
        }

        [Test]
        public void GetHashCode1_hash_code_of_similar_tuples_is_the_smae()
        {
            Assert.That(_tuple1.GetHashCode(), Is.EqualTo(_tuple1alt.GetHashCode()));
        }

        #endregion

        #region Tuple2

        [Test]
        public void Equals2_not_equals_to_null()
        {
            Assert.That(_tuple2.Equals(null), Is.False);
        }

        [Test]
        public void Equals2_not_equals_to_other_type()
        {
            Assert.That(_tuple2.Equals(123), Is.False);
        }

        [Test]
        public void Equals2_not_equals_to_different_tuple()
        {
            Assert.That(_tuple2.Equals(_tuple2dif1), Is.False);
            Assert.That(_tuple2.Equals(_tuple2dif2), Is.False);
        }

        [Test]
        public void Equals2_equals_to_similar_tuple()
        {
            Assert.That(_tuple2.Equals(_tuple2alt), Is.True);
        }

        [Test]
        public void GetHashCode2_hash_code_of_similar_tuples_is_the_smae()
        {
            Assert.That(_tuple2.GetHashCode(), Is.EqualTo(_tuple2alt.GetHashCode()));
        }

        #endregion

        #region Tuple8

        [Test]
        public void Equals8_not_equals_to_null()
        {
            Assert.That(_tuple8.Equals(null), Is.False);
        }

        [Test]
        public void Equals8_not_equals_to_other_type()
        {
            Assert.That(_tuple8.Equals(123), Is.False);
        }

        [Test]
        public void Equals8_not_equals_to_different_tuple()
        {
            Assert.That(_tuple8.Equals(_tuple8dif1), Is.False);
            Assert.That(_tuple8.Equals(_tuple8dif2), Is.False);
            Assert.That(_tuple8.Equals(_tuple8dif3), Is.False);
            Assert.That(_tuple8.Equals(_tuple8dif4), Is.False);
            Assert.That(_tuple8.Equals(_tuple8dif5), Is.False);
            Assert.That(_tuple8.Equals(_tuple8dif6), Is.False);
            Assert.That(_tuple8.Equals(_tuple8dif7), Is.False);
            Assert.That(_tuple8.Equals(_tuple8dif8), Is.False);
        }

        [Test]
        public void Equals8_equals_to_similar_tuple()
        {
            Assert.That(_tuple8.Equals(_tuple8alt), Is.True);
        }

        [Test]
        public void GetHashCode8_hash_code_of_similar_tuples_is_the_smae()
        {
            Assert.That(_tuple8.GetHashCode(), Is.EqualTo(_tuple8alt.GetHashCode()));
        }

        #endregion

        #endregion

        #region ToString

        [Test]
        public void ToString1_produces_correct_string()
        {
            Assert.That(_tuple1.ToString(), Is.EqualTo("(1)"));
        }

        [Test]
        public void ToString2_produces_correct_string()
        {
            Assert.That(_tuple2.ToString(), Is.EqualTo("(1, m)"));
        }

        [Test]
        public void ToString8_produces_correct_string()
        {
            Assert.That(_tuple8.ToString(), Is.EqualTo("(1, m, 1.1, m, 1, #m, 1.1, *m)"));
        }

        #endregion

        #region IComparable.CompareTo

        #region Tuple1

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void IComparable_CompareTo1_wrong_type_throws_ArgumentException()
        {
            ((IComparable)_tuple1).CompareTo(123);
        }

        [Test]
        public void IComparable_CompareTo1_not_null_greater_than_null()
        {
            Assert.That(((IComparable)_tuple1).CompareTo(null), Is.GreaterThan(0));
        }

        [Test]
        public void IComparable_CompareTo1_lexicographic_comparison_greater_than()
        {
            Assert.That(((IComparable)_tuple1).CompareTo(_tuple1less1), Is.GreaterThan(0));
        }

        [Test]
        public void IComparable_CompareTo1_lexicographic_comparison_less_than()
        {
            Assert.That(((IComparable)_tuple1).CompareTo(_tuple1great1), Is.LessThan(0));
        }

        [Test]
        public void IComparable_CompareTo1_tuple_is_equal_to_itself()
        {
            Assert.That(((IComparable)_tuple1).CompareTo(_tuple1), Is.EqualTo(0));
        }

        [Test]
        public void IComparable_CompareTo1_similar_tupples_are_equal()
        {
            Assert.That(((IComparable)_tuple1).CompareTo(_tuple1alt), Is.EqualTo(0));
        }

        #endregion

        #region Tuple2

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void IComparable_CompareTo2_wrong_type_throws_ArgumentException()
        {
            ((IComparable)_tuple2).CompareTo(123);
        }

        [Test]
        public void IComparable_CompareTo2_not_null_greater_than_null()
        {
            Assert.That(((IComparable)_tuple2).CompareTo(null), Is.GreaterThan(0));
        }

        [Test]
        public void IComparable_CompareTo2_lexicographic_comparison_greater_than()
        {
            Assert.That(((IComparable)_tuple2).CompareTo(_tuple2less1), Is.GreaterThan(0));
            Assert.That(((IComparable)_tuple2).CompareTo(_tuple2less2), Is.GreaterThan(0));
        }

        [Test]
        public void IComparable_CompareTo2_lexicographic_comparison_less_than()
        {
            Assert.That(((IComparable)_tuple2).CompareTo(_tuple2great1), Is.LessThan(0));
            Assert.That(((IComparable)_tuple2).CompareTo(_tuple2great2), Is.LessThan(0));
        }

        [Test]
        public void IComparable_CompareTo2_tuple_is_equal_to_itself()
        {
            Assert.That(((IComparable)_tuple2).CompareTo(_tuple2), Is.EqualTo(0));
        }

        [Test]
        public void IComparable_CompareTo2_similar_tupples_are_equal()
        {
            Assert.That(((IComparable)_tuple2).CompareTo(_tuple2alt), Is.EqualTo(0));
        }

        #endregion

        #region Tuple8

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void IComparable_CompareTo8_wrong_type_throws_ArgumentException()
        {
            ((IComparable)_tuple8).CompareTo(123);
        }

        [Test]
        public void IComparable_CompareTo8_not_null_greater_than_null()
        {
            Assert.That(((IComparable)_tuple8).CompareTo(null), Is.GreaterThan(0));
        }

        [Test]
        public void IComparable_CompareTo8_lexicographic_comparison_greater_than()
        {
            Assert.That(((IComparable)_tuple8).CompareTo(_tuple8less1), Is.GreaterThan(0));
            Assert.That(((IComparable)_tuple8).CompareTo(_tuple8less2), Is.GreaterThan(0));
            Assert.That(((IComparable)_tuple8).CompareTo(_tuple8less3), Is.GreaterThan(0));
            Assert.That(((IComparable)_tuple8).CompareTo(_tuple8less4), Is.GreaterThan(0));
            Assert.That(((IComparable)_tuple8).CompareTo(_tuple8less5), Is.GreaterThan(0));
            Assert.That(((IComparable)_tuple8).CompareTo(_tuple8less6), Is.GreaterThan(0));
            Assert.That(((IComparable)_tuple8).CompareTo(_tuple8less7), Is.GreaterThan(0));
            Assert.That(((IComparable)_tuple8).CompareTo(_tuple8less8), Is.GreaterThan(0));
        }

        [Test]
        public void IComparable_CompareTo8_lexicographic_comparison_less_than()
        {
            Assert.That(((IComparable)_tuple8).CompareTo(_tuple8great1), Is.LessThan(0));
            Assert.That(((IComparable)_tuple8).CompareTo(_tuple8great2), Is.LessThan(0));
            Assert.That(((IComparable)_tuple8).CompareTo(_tuple8great3), Is.LessThan(0));
            Assert.That(((IComparable)_tuple8).CompareTo(_tuple8great4), Is.LessThan(0));
            Assert.That(((IComparable)_tuple8).CompareTo(_tuple8great5), Is.LessThan(0));
            Assert.That(((IComparable)_tuple8).CompareTo(_tuple8great6), Is.LessThan(0));
            Assert.That(((IComparable)_tuple8).CompareTo(_tuple8great7), Is.LessThan(0));
            Assert.That(((IComparable)_tuple8).CompareTo(_tuple8great8), Is.LessThan(0));
        }

        [Test]
        public void IComparable_CompareTo8_tuple_is_equal_to_itself()
        {
            Assert.That(((IComparable)_tuple8).CompareTo(_tuple8), Is.EqualTo(0));
        }

        [Test]
        public void IComparable_CompareTo8_similar_tupples_are_equal()
        {
            Assert.That(((IComparable)_tuple8).CompareTo(_tuple8alt), Is.EqualTo(0));
        }

        #endregion

        #endregion
    }
}
