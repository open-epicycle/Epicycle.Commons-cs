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

using System.Collections.Generic;
using System.Linq;

// Authors: untrots

namespace Epicycle.Commons.Collections
{
    /// <summary>
    /// Contains various IEnumerable and IEnumerator related utilities.
    /// </summary>
    public static class EnumerationUtils
    {
        /// <summary>
        /// Concatinates all the enumerables into one enumerable.
        /// </summary>
        /// <typeparam name="T">The type of the enumerated object</typeparam>
        /// <param name="enumerables">The enumerables to concatinate. Must not be null or contain null enumerables</param>
        /// <returns>The concatinated enumerable</returns>
        public static IEnumerable<T> Concat<T>(IEnumerable<IEnumerable<T>> enumerables)
        {
            ArgAssert.NoNullIn(enumerables, "enumerables");

            var result = Enumerable.Empty<T>();

            foreach (var enumerable in enumerables)
            {
                result = result.Concat(enumerable);
            }

            return result;
        }
    }
}
