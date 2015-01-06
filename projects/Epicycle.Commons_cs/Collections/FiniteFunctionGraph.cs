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

namespace Epicycle.Commons.Collections
{
    public sealed class FiniteFunctionGraph<T1, T2> : IEnumerable<Tuple<T1, T2>>
    {
        public FiniteFunctionGraph(IReadOnlyList<T1> domain, IReadOnlyList<T2> codomain, IReadOnlyList<int> function)
        {
            ArgAssert.Equals(domain.Count, function.Count);

            _domain = domain;
            _codomain = codomain;
            _function = function;
        }

        private readonly IReadOnlyList<T1> _domain;
        private readonly IReadOnlyList<T2> _codomain;
        private readonly IReadOnlyList<int> _function;

        public IEnumerator<Tuple<T1, T2>> GetEnumerator()
        {
            for (var i = 0; i < _domain.Count; i++)
            {
                yield return Tuple.Create(_domain[i], _codomain[_function[i]]);
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
