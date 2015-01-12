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
using System.Collections.Generic;
using System.Linq;

namespace Epicycle.Commons.Collections
{
    [TestFixture]
    public sealed class CollectionUtilsTest : AssertionHelper
    {
        private List<int> _list;

        [SetUp]
        public void SetUp()
        {
            _list = new List<int> { 1, 2, 3 };
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
        public void AsReadOnlyList_IEnumerable_resulting_list_is_the_same()
        {
            AssertListEquals(_list, ((IEnumerable<int>)_list).AsReadOnlyList());
        }

        [Test]
        public void AsReadOnlyList_IList_resulting_list_is_the_same()
        {
            AssertListEquals(_list, ((IList<int>)_list).AsReadOnlyList());
        }

        [Test]
        public void AsReadOnlyList_List_resulting_list_is_the_same()
        {
            AssertListEquals(_list, ((List<int>)_list).AsReadOnlyList());
        }

        [Test]
        public void Last_returns_last_element_of_a_list()
        {
            Assert.AreEqual(_list[_list.Count - 1], _list.Last());
        }

        [Test]
        public void RemoveLast_removes_last_element_from_list()
        {
            _list.RemoveLast();
            AssertListEquals(new List<int> { 1, 2 }, (IList<int>)_list);
        }

        [Test]
        public void ElementAtCyclic_large_negative_to_large_positive_indices_give_correct_element()
        {
            for(var i = -100; i < 100; i++)
            {
                Assert.AreEqual(_list[i.Mod(_list.Count)], _list.AsReadOnlyList().ElementAtCyclic(i));
            }
        }

        [Test]
        public void AsSingleton_wraps_item_with_a_single_element_list()
        {
            AssertListEquals(new List<int> { 123 }, (123).AsSingleton());
        }

        [Test]
        public void Reverse_returns_reversed_list()
        {
            AssertListEquals(new List<int> { 3, 2, 1 }, _list.AsReadOnlyList().Reverse());
        }

        [Test]
        public void AsReverse_returns_reversed_list()
        {
            AssertListEquals(new List<int> { 3, 2, 1 }, _list.AsReadOnlyList().AsReverse());
        }

        [Test]
        public void AsCyclicPermutation_positive_shift_gives_correct_permutation()
        {
            AssertListEquals(new List<int> { 2, 3, 1 }, _list.AsReadOnlyList().AsCyclicPermutation(2));
        }

        [Test]
        public void AsCyclicPermutation_negative_shift_gives_correct_permutation()
        {
            AssertListEquals(new List<int> { 3, 1, 2 }, _list.AsReadOnlyList().AsCyclicPermutation(-2));
        }

        [Test]
        public void SieveByIndex_adds_list_elements_at_received_indices_to_one_collection_and_other_elements_to_another_collection()
        {
            var list = new List<int> { 1, 2, 4, 8, 16, 32, 64, 128, 256 };

            var indices = new List<int> { 0, 1, 3, 6 };

            var actualInside = new List<int>();
            var actualOutside = new List<int>();

            list.AsReadOnlyList().SieveByIndex(indices, actualInside, actualOutside);

            var expectedInside = indices.Select(i => list[i]).ToList();
            var expectedOutside = new List<int> { 4, 16, 32, 128, 256 };

            Expect(actualInside, Is.EquivalentTo(expectedInside));
            Expect(actualOutside, Is.EquivalentTo(expectedOutside));

            actualOutside.Clear();

            list.AsReadOnlyList().SieveByIndex(indices, actualOutside);

            Expect(actualOutside, Is.EquivalentTo(expectedOutside));
        }
    }
}
