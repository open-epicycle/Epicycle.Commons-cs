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

namespace Epicycle.Commons.Collections
{
    public sealed class InjectionEnumerator
    {
        public InjectionEnumerator(int domainCount, int codomainCount)
        {
            ArgAssert.AtMost(domainCount, "domainCount", codomainCount, "codomainCount");
            ArgAssert.AtLeast(domainCount, "domainCount", 1);

            _domainCount = domainCount;
            _codomainCount = codomainCount;

            _reset = true;
            
            _counters = new int[_domainCount];
            _free = new bool[_codomainCount];
        }

        private readonly int _domainCount;
        private readonly int _codomainCount;

        private bool _reset;

        private readonly int[] _counters;
        private readonly bool[] _free;

        public void Reset()
        {
            _reset = true;
        }

        public bool MoveNext()
        {
            if (_reset)
            {
                for (var i = 0; i < _domainCount; i++)
                {
                    _counters[i] = 0;
                }

                _reset = false;

                return true;
            }

            var k = _domainCount - 1;

            for (; k >= 1; k--)
            {
                if (_counters[k] < _codomainCount - k - 1)
                {
                    _counters[k]++;
                    break;
                }

                _counters[k] = 0;
            }

            if (k == 0)
            {
                _counters[0]++;
            }

            return _counters[0] < _codomainCount;
        }

        public void GetCurrent(IList<int> injection)
        {
            if (_reset || _counters[0] >= _codomainCount)
            {
                throw new InvalidOperationException();
            }

            for (var j = 0; j < _codomainCount; j++)
            {
                _free[j] = true;
            }

            for (var i = 0; i < _domainCount; i++)
            {
                injection[i] = -1;
                var counter = _counters[i];

                do
                {
                    injection[i]++;

                    if (_free[injection[i]])
                    {
                        counter--;
                    }
                } while (counter >= 0);

                _free[injection[i]] = false;
            }
        }
    }
}
