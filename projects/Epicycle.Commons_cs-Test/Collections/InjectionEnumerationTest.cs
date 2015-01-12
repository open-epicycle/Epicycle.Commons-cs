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

namespace Epicycle.Commons.Collections
{
    [TestFixture]
    public sealed class InjectionEnumerationTest : AssertionHelper
    {
        [Test]
        public void Injections_of_2_elements_into_3_elements_are_enumerated_correctly()
        {
            var enumer = new InjectionEnumerator(2, 3);

            var injection = new int[2];

            Expect(enumer.MoveNext(), Is.True);
            enumer.GetCurrent(injection);
            Expect(injection[0], Is.EqualTo(0));
            Expect(injection[1], Is.EqualTo(1));

            Expect(enumer.MoveNext(), Is.True);
            enumer.GetCurrent(injection);
            Expect(injection[0], Is.EqualTo(0));
            Expect(injection[1], Is.EqualTo(2));

            Expect(enumer.MoveNext(), Is.True);
            enumer.GetCurrent(injection);
            Expect(injection[0], Is.EqualTo(1));
            Expect(injection[1], Is.EqualTo(0));

            Expect(enumer.MoveNext(), Is.True);
            enumer.GetCurrent(injection);
            Expect(injection[0], Is.EqualTo(1));
            Expect(injection[1], Is.EqualTo(2));

            Expect(enumer.MoveNext(), Is.True);
            enumer.GetCurrent(injection);
            Expect(injection[0], Is.EqualTo(2));
            Expect(injection[1], Is.EqualTo(0));

            Expect(enumer.MoveNext(), Is.True);
            enumer.GetCurrent(injection);
            Expect(injection[0], Is.EqualTo(2));
            Expect(injection[1], Is.EqualTo(1));

            Expect(enumer.MoveNext(), Is.False);
        }
    }
}
