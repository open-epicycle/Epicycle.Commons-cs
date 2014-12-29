using System;
using System.Collections.Generic;
using System.Linq;

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
