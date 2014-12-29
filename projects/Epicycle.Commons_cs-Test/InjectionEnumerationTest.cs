using System;
using System.Collections.Generic;
using System.Linq;

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
