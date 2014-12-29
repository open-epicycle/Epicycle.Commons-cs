using System;
using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;

namespace Epicycle.Commons.Collections
{
    [TestFixture]
    public sealed class CollectionUtilsTest : AssertionHelper
    {
        [Test]
        public void SieveByIndex_adds_list_elements_at_received_indices_to_one_collection_and_other_elements_to_another_collection()
        {
            var list = new List<int> { 1, 2, 4, 8, 16, 32, 64, 128, 256 };

            var indices = new List<int> { 0, 1, 3, 6 };

            var actualInside = new List<int>();
            var actualOutside = new List<int>();

            list.SieveByIndex(indices, actualInside, actualOutside);

            var expectedInside = indices.Select(i => list[i]).ToList();
            var expectedOutside = new List<int> { 4, 16, 32, 128, 256 };

            Expect(actualInside, Is.EquivalentTo(expectedInside));
            Expect(actualOutside, Is.EquivalentTo(expectedOutside));

            actualOutside.Clear();

            list.SieveByIndex(indices, actualOutside);

            Expect(actualOutside, Is.EquivalentTo(expectedOutside));
        }
    }
}
