// [[[[INFO>
// Copyright 2014 Epicycle (http://epicycle.org)
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
using System.Linq;

namespace Epicycle.Commons.Collections
{
    [TestFixture]
    public class EnumerationUtilsTest
    {
        [Test]
        public void Concat_no_enumerables_produce_empty_enumerable()
        {
            Assert.That(EnumerationUtils.Concat(new int[][] { }).ToArray(), Is.EqualTo(new string[] { }));
        }

        [Test]
        public void Concat_one_enumerable_produces_it_data()
        {
            Assert.That(EnumerationUtils.Concat(new int[][] {new int[] { 1, 2, 3 }}).ToArray(), Is.EqualTo(new int[] { 1, 2, 3 }));
        }

        [Test]
        public void Concat_multiple_enumerables_produce_merged_enumerable()
        {
            Assert.That(EnumerationUtils.Concat(new string[][] {
                new string[] { "a", "b", "c" }, 
                new string[] { "u", "v", "w" }, 
                new string[] { "x", "y", "z" }}).ToArray(), 
                Is.EqualTo(new string[] { "a", "b", "c", "u", "v", "w", "x", "y", "z" }));
        }
    }
}
