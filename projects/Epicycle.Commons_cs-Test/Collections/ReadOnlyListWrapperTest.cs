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

using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;

namespace Epicycle.Commons.Collections
{
    [TestFixture]
    public class ReadOnlyListWrapperTest
    {
        private List<int> _emptyList;
        private List<int> _list;

        private ReadOnlyListWrapper<int> _wrappedEmptyList;
        private ReadOnlyListWrapper<int> _wrappedList;

        [SetUp]
        public void SetUp()
        {
            _emptyList = new List<int> { };
            _list = new List<int> { 1, 2, 3 };

            _wrappedEmptyList = new ReadOnlyListWrapper<int>(_emptyList);
            _wrappedList = new ReadOnlyListWrapper<int>(_list);
        }

        private void AssertListEquals<T>(IList<T> expected, IReadOnlyList<T> list)
        {
            Assert.AreEqual(expected.Count, list.Count);
            for (var i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i], list[i]);
            }
        }

        private void AssertListEquals<T>(IList<T> expected, IList<T> list)
        {
            Assert.AreEqual(expected.Count, list.Count);
            for (var i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i], list[i]);
            }
        }

        [Test]
        public void Count_empty_list_yields_zero_elements()
        {
            Assert.That(_wrappedEmptyList.Count, Is.EqualTo(0));
        }

        [Test]
        public void Count_non_empty_list_yields_correct_number_of_elements()
        {
            Assert.That(_wrappedList.Count, Is.EqualTo(_list.Count));
        }

        [Test]
        public void Item_yields_correct_list_items()
        {
            for (var i = 0; i < _list.Count; i++)
            {
                Assert.That(_wrappedList[i], Is.EqualTo(_list[i]));
            }
        }

        [Test]
        public void GetEnumerator_enumerates_correct_items()
        {
            var listEnumerator = _list.GetEnumerator();
            var wrappedListEnumerator = _wrappedList.GetEnumerator();

            ValidateEnumerator(listEnumerator, wrappedListEnumerator);
        }

        [Test]
        public void IEnumerable_GetEnumerator_enumerates_correct_items()
        {
            var listEnumerator = ((IEnumerable)_list).GetEnumerator();
            var wrappedListEnumerator = ((IEnumerable)_wrappedList).GetEnumerator();

            ValidateEnumerator(listEnumerator, wrappedListEnumerator);
        }

        private void ValidateEnumerator(IEnumerator expected, IEnumerator enumerator)
        {
            bool result;
            while (expected.MoveNext())
            {
                result = enumerator.MoveNext();

                Assert.That(result, Is.True);
                Assert.That(enumerator.Current, Is.EqualTo(expected.Current));
            }

            result = enumerator.MoveNext();
            Assert.That(result, Is.False);
        }
    }
}
