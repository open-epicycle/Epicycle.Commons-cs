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
        private Tuple<int, string> _tuple2;
        private Tuple<int, string> _tuple2alt;
        private Tuple<int, string> _tuple2dif1;
        private Tuple<int, string> _tuple2dif2;
        private Tuple<int, string> _tuple2less1;
        private Tuple<int, string> _tuple2less2;
        private Tuple<int, string> _tuple2great1;
        private Tuple<int, string> _tuple2great2;

        [SetUp]
        public void SetuUp()
        {
            _tuple2 = Tuple.Create(1, "m");
            _tuple2alt = Tuple.Create(1, "m");
            _tuple2dif1 = Tuple.Create(2, "m");
            _tuple2dif2 = Tuple.Create(1, "X");
            _tuple2less1 = Tuple.Create(0, "m");
            _tuple2less2 = Tuple.Create(1, "a");
            _tuple2great1 = Tuple.Create(2, "m");
            _tuple2great2 = Tuple.Create(1, "z");
        }

        #region Create

        [Test]
        public void Create2_creates_tuple_with_correct_items()
        {
            Assert.That(_tuple2.Item1, Is.EqualTo(1));
            Assert.That(_tuple2.Item2, Is.EqualTo("m"));
        }

        #endregion

        #region Equals & GetHashCode

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
            Assert.That(_tuple2.Equals(_tuple2dif2), Is.False);
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

        #region ToString

        [Test]
        public void ToString2_produces_correct_string()
        {
            Assert.That(_tuple2.ToString(), Is.EqualTo("(1, m)"));
        }

        #endregion

        #region IComparable.CompareTo

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
    }
}
