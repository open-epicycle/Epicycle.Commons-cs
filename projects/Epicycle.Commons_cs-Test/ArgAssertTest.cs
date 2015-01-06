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
using System;

namespace Epicycle.Commons
{
    [TestFixture]
    class ArgAssertTest
    {
        [Test]
        public void NotNull_not_null_does_nothing()
        {
            ArgAssert.NotNull("foo", "someArg");
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NotNull_null_throws_NullReferenceException()
        {
            ArgAssert.NotNull(null, "someArg");
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NoNullIn_null_argument_throws_NullReferenceException()
        {
            string[] arg = null;
            ArgAssert.NoNullIn(arg, "someArg");
        }

        [Test]
        public void NoNullIn_empty_array_does_nothing()
        {
            ArgAssert.NoNullIn(new string[0], "someArg");
        }

        [Test]
        public void NoNullIn_array_without_nulls_does_nothing()
        {
            ArgAssert.NoNullIn(new string[] { "aa", "bb" }, "someArg");
        }

        [Test]
        [ExpectedException(typeof(NullReferenceException))]
        public void NoNullIn_array_with_nulls_throws_NullReferenceException()
        {
            ArgAssert.NoNullIn(new string[] { "aa", null, "bb" }, "someArg");
        }
    }
}
