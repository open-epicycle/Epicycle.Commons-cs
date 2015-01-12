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
using System;
using System.Collections.Generic;
using System.Linq;

namespace Epicycle.Commons.Collections
{
    [TestFixture]
    public class FiniteFunctionGraphTest
    {
        private List<int> _domain;
        private List<string> _codomain;
        private List<int> _function;
        private FiniteFunctionGraph<int, string> _graph;

        [SetUp]
        public void SetUp()
        {
            _domain = new List<int> { };
            _function = new List<int> { };
            _codomain = new List<string> { "a", "b", "c", "d" };

            Rebuild();
        }

        private void Rebuild()
        {
            _graph = new FiniteFunctionGraph<int, string>(_domain.AsReadOnlyList(), _codomain.AsReadOnlyList(), _function.AsReadOnlyList());
        }

        [Test]
        public void empty_domain_yields_empty_graph()
        {
            Assert.That(_graph.ToArray(), Is.EqualTo(new Tuple<int, string>[0]));
        }

        [Test]
        public void non_empty_domain_yields_correct_graph()
        {
            _domain = new List<int> { 1, 2, 3, 40, 5 };
            _function = new List<int> { 3, 3, 0, 2, 0 };

            var expected = new List<Tuple<int, string>>
            {
                Tuple.Create(1, "d"),
                Tuple.Create(2, "d"),
                Tuple.Create(3, "a"),
                Tuple.Create(40, "c"),
                Tuple.Create(5, "a"),
            };

            Rebuild();

            Assert.That(_graph.ToArray(), Is.EqualTo(expected.ToArray()));
        }
    }
}
